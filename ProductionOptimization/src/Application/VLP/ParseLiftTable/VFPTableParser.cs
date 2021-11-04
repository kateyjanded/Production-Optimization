using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class VFPTableParser
    {
        private string filename;

        public VFPTableParser(string filename)
        {
            this.filename = filename;
        }

        public VFPTableParser() { }

        public VFPTableParser SetInputFile(string filename)
        {
            this.filename = filename;
            return this;
        }

        //private string ReadRecordString(StreamReader reader)
        //{
        //    var vector = "";

        //    while (true)
        //    {
        //        string line = reader.ReadLine();

        //        if (line == null || line.Trim() == "/")
        //        {
        //            return vector;
        //        }

        //        line = line.Trim();

        //        if (line == "")
        //        {
        //            return vector;
        //        }

        //        if (line.EndsWith("/"))
        //        {
        //            return vector + " " + line.Remove(line.Length - 1, 1);
        //        }

        //        vector = vector + " " + line;
        //    }

        //}

        //private List<string> ReadVector(StreamReader reader)
        //{
        //    string vector = ReadRecordString(reader);

        //    if (vector == "")
        //    {
        //        return null;
        //    }

        //    return vector.Split().ToList();
        //}

        private bool IsRecordStart(string line)
        {
            string pattern = @"^\s*\d+";

            var re = new Regex(pattern);

            return re.IsMatch(line);
        }

        private List<string> ReadRecordString(StreamReader reader)
        {
            //string pattern = @"^\s*\d+";
            //var re = new Regex(pattern);

            string result = "";

            string line = reader.ReadLine();

            while (line != null && !IsRecordStart(line.Trim())) // skip to beginning of record
            {
                line = reader.ReadLine();
            }

            // line now contains first line of record string
            // add further lines until end-of-record marker ("/") is reached
            while (line != null && !line.Trim().EndsWith("/"))
            {
                result = result + " " + line.Trim();
                line = reader.ReadLine();
            }

            if (line != null && line.Trim().Length > 1)
            {
                line = line.Trim();
                result = result + " " + line.Remove(line.Length - 1, 1); // remove terminating "/"
            }

            var reg = new Regex(@"\'\s\'");
            result = reg.Replace(result, "X");
            return result.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).ToList();
        }


        private Header ParseHeader(List<string> header)
        {
            if (header.Count < 4)
            {
                throw new Exception("Header record not complete or found");
            }

            var headerObject = new Header();

            var wfr = new List<string> { "WCT", "WOR", "WGR" };
            var gfr = new List<string> { "GOR", "GLR", "OGR" };
            var alq = new List<string> { "GRAT", "IGLR", "TGLR", "COMP", "PUMP", "DENO", "DENG" };

            headerObject.TableNo = Int32.Parse(header[0]);
            headerObject.ReferenceDepth = Double.Parse(header[1]);
            headerObject.FLOType = header[2].Trim('"').Trim('\'');

            if (header.Count > 3)
            {
                headerObject.WFRType = header[3].Trim('"').Trim('\'');
            }

            if (header.Count > 4)
            {
                headerObject.GFRType = header[4].Trim('"').Trim('\'');
            }

            // if (header.Count > 5) -- no need to check for thp since it has to be thp.

            if (header.Count > 6)
            {
                headerObject.ALQType = header[6].Trim('"').Trim('\'');
            }

            if (header.Count > 7)
            {
                headerObject.UnitType = header[7].Trim('"').Trim('\'');
            }

            if (header.Count > 8)
            {
                headerObject.TabType = header[8].Trim('"').Trim('\'');
            }
            return headerObject;
        }

        public List<double> ParseToDoubleVector(List<string> recordStringList)
        {
            if (recordStringList.Count == 0)
            {
                throw new Exception("Record is empty!");
            }

            var result = new List<double>();

            foreach (var e in recordStringList)
            {
                result.Add(Double.Parse(e));
            }

            return result;
        }
        public List<string> TestReadRecord()
        {
            var reader = new StreamReader(this.filename);
            return ReadRecordString(reader);
        }

        public VFPTable ParseVFPTable()
        {
            var idx = 0;

            var positions = new List<int> { 0 };

            var shape = new List<int>();

            var axesDict = new Dictionary<TableColumn, List<double>>();

            var reader = new StreamReader(this.filename);

            var headerStringList = ReadRecordString(reader);

            var header = ParseHeader(headerStringList);

            var floRecord = ReadRecordString(reader);

            var floAxis = ParseToDoubleVector(floRecord);

            //shape.Add(floAxis.Count);

            var thpRecord = ReadRecordString(reader);

            var thpAxis = ParseToDoubleVector(thpRecord);

            axesDict.Add(new TableColumn("THP", "THP", 0), thpAxis);

            shape.Add(thpAxis.Count);

            var wfrRecord = ReadRecordString(reader);

            if (wfrRecord.Count > 1)
            {
                var wfrAxis = ParseToDoubleVector(wfrRecord);
                idx++;
                axesDict.Add(new TableColumn(header.WFRType, "WFR", idx), wfrAxis);
                positions.Add(1);
                shape.Add(wfrAxis.Count);
            }

            var gfrRecord = ReadRecordString(reader);

            if (gfrRecord.Count > 1)
            {
                var gfrAxis = ParseToDoubleVector(gfrRecord);
                idx++;
                axesDict.Add(new TableColumn(header.GFRType, "GFR", idx), gfrAxis);
                positions.Add(2);
                shape.Add(gfrAxis.Count);
            }

            var alqRecord = ReadRecordString(reader);

            if (alqRecord.Count > 1)
            {
                var alqAxis = ParseToDoubleVector(alqRecord);
                idx++;
                axesDict.Add(new TableColumn(header.ALQType, "ALQ", idx), alqAxis);
                positions.Add(3);
                shape.Add(alqAxis.Count);
            }

            idx++;

            axesDict.Add(new TableColumn(header.FLOType, "FLO", idx), floAxis);
            shape.Add(floAxis.Count);

            //------------------------------read record 10--------------------------------------------

            var record = ReadRecordString(reader);

            var table = new NDimensionalArray(shape.ToArray());

            while (record.Count != 0)
            {
                var indexList = new List<int>();
                foreach (var p in positions)
                {
                    indexList.Add(Int32.Parse(record[p]) - 1);
                }

                for (int i = 4; i < record.Count; ++i)
                {
                    indexList.Add(i - 4);
                    var indexTuple = new Tuple(indexList);
                    table[indexTuple] = Double.Parse(record[i]);
                    indexList.RemoveAt(indexList.Count - 1);
                }

                record = ReadRecordString(reader);
            }

            var vfpTable = new VFPTable(header, axesDict, table);

            return vfpTable;


        }

        //-----------------------------------------------------------------------------------------------------------------------
        //New methods added so that parser can work with lift table in form of string instead of an ascii file.
        //-----------------------------------------------------------------------------------------------------------------------


        private System.Tuple<List<string>, int> ReadRecordString(string[] lines, int currentPosition)
        {
            string result = "";

            //string line = reader.ReadLine();

            string line = lines[currentPosition++];
            int end = lines.Length;

            while (currentPosition != end && !IsRecordStart(line.Trim())) // skip to beginning of record
            {
                line = lines[currentPosition++];
            }

            // line now contains first line of record string
            // add further lines until end-of-record marker ("/") is reached
            while (currentPosition != end && !line.Trim().EndsWith("/"))
            {
                result = result + " " + line.Trim();
                line = lines[currentPosition++];
            }

            if (currentPosition != end && line.Trim().Length > 1)
            {
                line = line.Trim();
                result = result + " " + line.Remove(line.Length - 1, 1); // remove terminating "/"
            }

            var reg = new Regex(@"\'\s\'");
            result = reg.Replace(result, "X");

            return System.Tuple.Create(result.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).ToList(), currentPosition);
        }

        private int NextRecordPosition(string[] lines, int currentPosition)
        {
            string line = lines[currentPosition++];
            int end = lines.Length;

            while (currentPosition != end && !IsRecordStart(line.Trim())) // skip to beginning of record
            {
                line = lines[currentPosition++];
            }

            return currentPosition - 1;
        }
        private System.Tuple<List<string>, int> ReadRecordString1(string[] lines, int currentPosition)
        {
            string result = "";

            //string line = reader.ReadLine();

            string line = lines[currentPosition++];
            int end = lines.Length;

            //while (currentPosition != end && !IsRecordStart(line.Trim())) // skip to beginning of record
            //{
            //    line = lines[currentPosition++];
            //}

            // line now contains first line of record string
            // add further lines until end-of-record marker ("/") is reached
            while (currentPosition != end && !line.Trim().EndsWith("/"))
            {
                result = result + " " + line.Trim();
                line = lines[currentPosition++];
            }

            if (currentPosition != end && line.Trim().Length > 1)
            {
                line = line.Trim();
                result = result + " " + line.Remove(line.Length - 1, 1); // remove terminating "/"
            }

            var reg = new Regex(@"\'\s\'");
            result = reg.Replace(result, "X");

            return System.Tuple.Create(result.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).ToList(), currentPosition);
        }


        public VFPTable ParseVFPTable(string vfpTbleString)
        {

            var vfpTableStringArray = vfpTbleString.Split('\n');
            var idx = 0;

            var positions = new List<int> { 0 };

            var shape = new List<int>();

            var axesDict = new Dictionary<TableColumn, List<double>>();

            //var reader = new StreamReader(this.filename);

            //var headerStringList = ReadRecordString(reader);
            List<string> headerStringList;
            int currentPosition;
            var temp = ReadRecordString(vfpTableStringArray, 0);
            headerStringList = temp.Item1;
            currentPosition = temp.Item2;

            var header = ParseHeader(headerStringList);

            var nextRecordStart = NextRecordPosition(vfpTableStringArray, currentPosition);

            var floRecordTuple = ReadRecordString1(vfpTableStringArray, nextRecordStart);
            var floRecord = floRecordTuple.Item1;
            currentPosition = floRecordTuple.Item2;

            var floAxis = ParseToDoubleVector(floRecord);

            var floUnit = ExtractUnit(vfpTableStringArray, nextRecordStart - 1);

            //shape.Add(floAxis.Count);

            nextRecordStart = NextRecordPosition(vfpTableStringArray, currentPosition);
            var thpRecordTuple = ReadRecordString1(vfpTableStringArray, nextRecordStart);
            var thpRecord = thpRecordTuple.Item1;
            currentPosition = thpRecordTuple.Item2;

            var thpAxis = ParseToDoubleVector(thpRecord);

            var thpUnit = ExtractUnit(vfpTableStringArray, nextRecordStart - 1);

            axesDict.Add(new TableColumn("THP", "THP", 0, thpUnit), thpAxis);

            shape.Add(thpAxis.Count);

            nextRecordStart = NextRecordPosition(vfpTableStringArray, currentPosition);
            var wfrRecordTuple = ReadRecordString1(vfpTableStringArray, nextRecordStart);
            var wfrRecord = wfrRecordTuple.Item1;
            currentPosition = wfrRecordTuple.Item2;

            if (wfrRecord.Count > 1)
            {
                var wfrAxis = ParseToDoubleVector(wfrRecord);
                idx++;
                var wfrUnit = ExtractUnit(vfpTableStringArray, nextRecordStart - 1);
                axesDict.Add(new TableColumn(header.WFRType, "WFR", idx, wfrUnit), wfrAxis);
                positions.Add(1);
                shape.Add(wfrAxis.Count);
            }

            nextRecordStart = NextRecordPosition(vfpTableStringArray, currentPosition);
            var gfrRecordTuple = ReadRecordString1(vfpTableStringArray, nextRecordStart);
            var gfrRecord = gfrRecordTuple.Item1;
            currentPosition = gfrRecordTuple.Item2;

            if (gfrRecord.Count > 1)
            {
                var gfrAxis = ParseToDoubleVector(gfrRecord);
                idx++;
                var gfrUnit = ExtractUnit(vfpTableStringArray, nextRecordStart - 1);
                axesDict.Add(new TableColumn(header.GFRType, "GFR", idx, gfrUnit), gfrAxis);
                positions.Add(2);
                shape.Add(gfrAxis.Count);
            }

            nextRecordStart = NextRecordPosition(vfpTableStringArray, currentPosition);
            var alqRecordTuple = ReadRecordString1(vfpTableStringArray, nextRecordStart);
            var alqRecord = alqRecordTuple.Item1;
            currentPosition = alqRecordTuple.Item2;

            if (alqRecord.Count > 1)
            {
                var alqAxis = ParseToDoubleVector(alqRecord);
                idx++;
                var alqUnit = ExtractUnit(vfpTableStringArray, nextRecordStart - 1);
                axesDict.Add(new TableColumn(header.ALQType, "ALQ", idx, alqUnit), alqAxis);
                positions.Add(3);
                shape.Add(alqAxis.Count);
            }

            idx++;

            axesDict.Add(new TableColumn(header.FLOType, "FLO", idx, floUnit), floAxis);
            shape.Add(floAxis.Count);

            //------------------------------read record 10--------------------------------------------

            var recordTuple = ReadRecordString(vfpTableStringArray, currentPosition);
            var record = recordTuple.Item1;
            currentPosition = recordTuple.Item2;

            var table = new NDimensionalArray(shape.ToArray());

            //while (record.Count != 0)
            while (currentPosition < vfpTableStringArray.Length)
            {
                var indexList = new List<int>();
                foreach (var p in positions)
                {
                    indexList.Add(Int32.Parse(record[p]) - 1);
                }

                for (int i = 4; i < record.Count; ++i)
                {
                    indexList.Add(i - 4);
                    var indexTuple = new Tuple(indexList);
                    table[indexTuple] = Double.Parse(record[i]);
                    indexList.RemoveAt(indexList.Count - 1);
                }
                recordTuple = ReadRecordString(vfpTableStringArray, currentPosition);
                record = recordTuple.Item1;
                currentPosition = recordTuple.Item2;
            }

            var vfpTable = new VFPTable(header, axesDict, table);

            return vfpTable;

        }

        private string ExtractUnit(string[] vfpTableStringArray, int position)
        {
            //"units - stb/day"
            var pattern = @"units\s*-\s*(\w+/?\w+)";

            var regex = new Regex(pattern);
            var line = vfpTableStringArray[position];
            var match = regex.Match(line);
            var unit = "";
            if (match.Success)
            {
                unit = match.Groups[1].Value;
            }

            return unit;
        }


    }
}
