using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
	public class UnitConverter
	{
		public static readonly double M = 1e3;
		public static Dictionary<string, Dictionary<string, double>> Factor =
						 new Dictionary<string, Dictionary<string, double>>
						 {
							 { "stb/MMscf", new Dictionary<string, double>
											{
												{"stb/Mscf",  1/M},
												{"stb/scf", 1/(M*M) },
												{"stb/MMscf", 1 }
											}
							 },
							 { "stb/Mscf", new Dictionary<string, double>
											{
												{"stb/MMscf", M },
												{"stb/scf", 1/M },
												{"stb/Mscf", 1 }
											}
							 },
							 { "stb/scf", new Dictionary<string, double>
											{
												{"stb/Mscf", M },
												{"stb/MMscf", M*M },
												{"stb/scf", 1}
											}
							 },
							 { "scf/stb", new Dictionary<string, double>
											{
												{"Mscf/stb", 1/M },
												{"MMscf/stb", 1/(M*M) },
												{"scf/stb", 1 }
											}
							 },
							 { "Mscf/stb", new Dictionary<string, double>
											{
												{"scf/stb", M },
												{"MMscf/stb", 1/M },
												{"Mscf/stb", 1 }
											}
							 },
							 { "MMscf/stb", new Dictionary<string, double>
											{
												{"scf/stb", M*M },
												{"Mscf/stb", M },
												{"MMscf/stb", 1 }
											}
							 },
							 { "stb/day", new Dictionary<string, double>
											{
												{"Mstb/day", 1/M },
												{"MMstb/day", 1/(M*M) },
												{ "stb/day", 1 }
											}
							 },
							 { "scf/day", new Dictionary<string, double>
											{
												{"Mscf/day", 1/M },
												{"MMscf/day", 1/(M*M) },
												{ "scf/day", 1 }
											}
							 },
							 { "Mscf/day", new Dictionary<string, double>
											{
												{"scf/day", M },
												{"MMscf/day", 1/M },
												{"Mscf/day", 1 }
											}
							 },
							 { "MMscf/day", new Dictionary<string, double>
											{
												{"scf/day", M*M },
												{"Mscf/day", M },
												{"MMscf/day", 1 }
											}
							 },
							 { "fraction", new Dictionary<string, double>
											{
												{"percent", 100 },
												{"fraction", 1 },
												{"stb/stb", 1 }
											}
							 },
							 { "percent", new Dictionary<string, double>
											{
												{"fraction", 1.0/100 },
												{"percent", 1 },
												{"stb/stb", 1.0/100 }
											}
							 },
							 { "stb/stb", new Dictionary<string, double>
											{
												{"percent", 100 },
												{"fraction", 1 },
												{"stb/stb", 1 }
											}
							 },
						 };
		public static double bbl_per_ft3_to_m3_per_m3(double input)
		{
			// function output = from_bbl_per_ft3_to_m3_per_m3(input)
			//
			// Performs unit conversion from bbl/ft^3 to m^3/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input * 5.6146;

		}
		public static double PsiaToPsig(string convertFromUnit, string convertToUnit)
		{
			if ((convertFromUnit).ToLower() == "psia" && (convertToUnit).ToLower() == "psig")
			{
				return 14.7;
			}
			else if ((convertFromUnit).ToLower() == "psig" && (convertToUnit).ToLower() == "psia")
			{
				return 14.7;
			}
			return 0;
		}
		public static double bbl_to_m3(double input)
		{
			// function output = fm_bbl_to_m3(input)
			//
			// Performs unit conversion from bbl to m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.589873e-1;

		}

		public static double bpd_psi_ft_to_m2_per_d_Pa(double input)
		{
			// function output = from_bpd_psi_ft_to_m2_per_d_Pa(input)
			//
			// Performs unit conversion from bbl/(d*psi*ft) to m^3/(d*Pa*m) = m^2/(d*Pa).
			//
			// Musa Mohamma, 21-10-2019

			return input * 7.565341e-5;

		}
		public static double bpd_psi_ft_to_m2_per_s_Pa(double input)
		{
			// function output = from_bpd_psi_ft_to_m2_per_s_Pa(input)
			//
			// Performs unit conversion from bbl/(d*psi*ft) to m^3/(s*Pa*m) = m^2/(s*Pa).
			//
			// Musa Mohamma, 21-10-2019

			return input * 8.756182e-10;

		}
		public static double bpd_psi_to_m3_per_d_Pa(double input)
		{
			// function output = from_bpd_psi_to_m3_per_d_Pa(input)
			//
			// Performs unit conversion from bbl/(d*psi) to m^3/(d*Pa).
			//
			// Musa Mohamma, 21-10-2019

			return input * 2.305916e-5;

		}
		public static double bpd_psi_to_m3_per_s_Pa(double input)
		{
			// function output = from_bpd_psi_to_m3_per_s_Pa(input)
			//
			// Performs unit conversion from bbl/(d*psi) to m^3/(s*Pa).
			//
			// Musa Mohamma, 21-10-2019

			return input * 2.668884e-10;

		}

		public static double bpd_to_ft3_per_s(double input)
		{
			// function output = from_bpd_to_m3_per_d(input)
			//
			// Performs unit conversion from bpd to m^3/d.
			//
			// This conversion was not in the original MATLAB
			// Added in by me:
			//
			// Musa Mohamma, 26-11-2020

			return input * 4.87377025e-5;
		}

		public static double bpd_to_m3_per_d(double input)
		{
			// function output = from_bpd_to_m3_per_d(input)
			//
			// Performs unit conversion from bpd to m^3/d.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.589873e-1;

		}
		public static double bpd_to_m3_per_s(double input)
		{
			// function output = from_bpd_to_m3_per_s(input)
			//
			// Performs unit conversion from bpd to m^3/s.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.840131e-6;

		}
		public static double cal_to_J(double input)
		{
			// function output = from_cal_to_J(input)
			//
			// Performs unit conversion from cal to J (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 4.184;

		}
		public static double cp_to_Pa_s(double input)
		{
			// function output = from_cp_to_Pa_s(input)
			//
			// Performs unit conversion from cp to Pa*s (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.0e-3;

		}
		public static double cSt_to_m2_per_s(double input)
		{
			// function output = from_cSt_to_m2_per_s(input)
			//
			// Performs unit conversion from cSt to m^2/s (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.0e-6;

		}
		public static double deg_API_to_kg_per_m3(double input)
		{
			// function output = from_deg_API_to_kg_per_m3(input)
			//
			// Performs unit conversion from degrees API to kg/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return 141.5e3 / (131.5 + input);

		}
		public static double deg_API_to_liq_grav(double input)
		{
			// function output = from_deg_API_to_liq_grav(input)
			//
			// Performs unit conversion from degrees API to liquid gravity (wrt. water = 1000 kg/m^3).
			//
			// Musa Mohamma, 21-10-2019

			return (141.5e3 / (131.5 + input)) / 1000;

		}
		public static double deg_C_to_deg_F(double input)
		{
			// function output = from_deg_C_to_deg_F(input)
			//
			// Performs unit conversion from degrees C to degrees F (exact).
			//
			// Musa Mohamma, 21-10-2019

			return 1.8 * input + 32;

		}
		public static double deg_F_to_deg_C(double input)
		{
			// function output = from_deg_F_to_deg_C(input)
			//
			// Performs unit conversion from degrees F to degrees C (exact).
			//
			// Musa Mohamma, 21-10-2019

			return (input - 32) / 1.8;

		}
		public static double deg_F_to_deg_R(double input)
		{
			// function output = from_deg_F_to_deg_R(input)
			//
			// Performs unit conversion from degrees F to degrees R (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input + 459.67;

		}
		public static double deg_R_to_K(double input)
		{
			// function output = from_deg_R_to_K(input)
			//
			// Performs unit conversion from degrees R to K (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 5.0 / 9.0;

		}
		public static double deg_to_rad(double input)
		{
			// function output = from_deg_to_rad(input)
			//
			// Performs conversion from degrees to radians.
			//
			// Musa Mohamma, 21-10-2019

			return input * Math.PI / 180;

		}
		public static double dyne_per_cm_to_N_per_m(double input)
		{
			// function output = from_dyne_per_cm_to_N_per_m(input)
			//
			// Performs unit conversion from dyne/cm to N/m (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 1e-3;

		}
		public static double ft2_to_m2(double input)
		{
			// function output = from_ft2_to_m2(input)
			//
			// Performs unit conversion from ft^2 to m^2 (exact).
			//
			// Musa Mohamma, 21-10-2019
			//
			return input * (3.048e-1) * (3.048e-1);

		}
		public static double ft3_per_bbl_to_m3_per_m3(double input)
		{
			// function output = from_ft3_per_bbl_to_m3_per_m3(input)
			//
			// Performs unit conversion from ft^3/bbl to m^3/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.781076e-1;

		}
		public static double ft3_per_d_to_m3_per_d(double input)
		{
			// function output = from_ft3_per_d_to_m3_per_d(input)
			//
			// Performs unit conversion from ft^3/d to m^3/d.
			//
			// Musa Mohamma, 21-10-2019

			return input * 2.831685e-2;

		}
		public static double ft3_per_d_to_m3_per_s(double input)
		{
			// function output = from_ft3_per_d_to_m3_per_s(input)
			//
			// Performs unit conversion from ft^3/d to m^3/s.
			//
			// Musa Mohamma, 21-10-2019

			return input * 3.277413e-7;

		}
		public static double ft3_per_s_to_m3_per_s(double input)
		{
			// function output = from_ft3_per_s_to_m3_per_s(input)
			//
			// Performs unit conversion from ft^3/s to m^3/s.
			//
			// Musa Mohamma, 21-10-2019

			return input * 2.831685e-2;

		}
		public static double ft3_to_m3(double input)
		{
			// function output = from_ft3_to_m3(input)
			//
			// Performs unit conversion from ft^3 to m^3 (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * (3.048e-1) * (3.048e-1) * (3.048e-1);

		}
		public static double ft_per_s_to_m_per_s(double input)
		{
			// function output = from_ft_per_s_to_m_per_s(input)
			//
			// Performs unit conversion from ft/s to m/s (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 3.048e-1;

		}
		public static double ft_to_m(double input)
		{
			// function output = from_ft_to_m(input)
			//
			// Performs unit conversion from ft to m (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 3.048e-1;

		}
		public static double gal_per_min_to_m3_per_s(double input)
		{
			// function output = from_gal_per_min_to_m3_per_s(input)
			//
			// Performs unit conversion from gal/min to m^3/s.
			//
			// Musa Mohamma, 21-10-2019

			return input * 6.309020e-5;

		}
		public static double gas_grav_to_kg_per_m3(double input)
		{
			// output = from_gas_grav_to_kg_per_m3(input)
			//
			// Performs conversion from specific gas gravity (density relative
			// to density of air at standard conditions = 1.23 kg/m^3 =
			// 76.3 * 10^-3 lbm/ft^3) to kg/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.23;

		}
		public static double gas_grav_to_molar_mass(double input)
		{
			// output = from_gas_grav_to_molar_mass(input)
			//
			// Performs conversion from specific gas gravity (density relative to density of air at standard
			// conditions = 1.23 kg/m^3 = 76.3 * 10^-3 lbm/ft^3) to molar mass expressed in kg/mol.
			//
			// Musa Mohamma, 21-10-2019

			return input * 28.97e-3;

		}
		public static double gas_grav_to_mol_weight(double input)
		{
			// output = from_gas_grav_to_mol_weight(input)
			//
			// Performs conversion from specific gas gravity (density relative to density of air at standard
			// conditions = 1.23 kg/m^3 = 76.3 * 10^-3 lbm/ft^3) to molecular weight expressed in g/mol
			// (lbm/lbm-mole).
			//
			// Musa Mohamma, 21-10-2019

			return input * 28.97;

		}
		public static double hp_to_W(double input)
		{
			// output = from_hp_to_W(input)
			//
			// Performs unit conversion from hp to W.
			//
			// Musa Mohamma, 21-10-2019

			return input * 7.456999;

		}
		public static double in2_to_m2(double input)
		{
			// output = from_in2_to_m2(input)
			//
			// Performs unit conversion from in^2 to m^2 (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * (2.54e-2) * (2.54e-2);

		}
		public static double in_to_m(double input)
		{
			// output = from_in_to_m(input)
			//
			// Performs unit conversion from in. to m (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 0.0254;

		}
		public static double J_to_cal(double input)
		{
			// output = from_J_to_cal(input)
			//
			// Performs unit conversion from J to cal (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / 4.184;

		}
		public static double kg_per_m3_to_deg_API(double input)
		{
			// output = from_kg_per_m3_to_deg_API(input)
			//
			// Performs unit conversion from kg/m^3 to degrees API.
			//
			// Musa Mohamma, 21-10-2019

			return (141.5e3 / input) - 131.5;

		}
		public static double kg_per_m3_to_gas_grav(double input)
		{
			// output = from_kg_per_m3_to_gas_grav(input)
			//
			// Performs conversion from kg/m^3 to specific gas gravity (density
			// relative to density of air at standard conditions = 1.23 kg/m^3 =
			// 76.3 * 10^-3 lbm/ft^3).
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.23;

		}
		public static double kg_per_m3_to_lbm_per_ft3(double input)
		{
			// output = from_kg_per_m3_to_lbm_per_ft3(input)
			//
			// Performs unit conversion from kg/m^3 to lbm/ft^3.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.601846e1;

		}
		public static double kg_per_m3_to_liq_grav(double input)
		{
			// output = from_kg_per_m3_to_liq_grav(input)
			//
			// Performs conversion from kg/m^3 to specific liquid gravity (density
			// relative to density of water at standard conditions = 999 kg/m^3 =
			// 62.4 lbm/ft^3).
			//
			// Musa Mohamma, 21-10-2019

			return input / 999.0;

		}
		public static double kg_per_m3_to_molar_mass(double input)
		{
			// output = from_kg_per_m3_to_molar_mass(input)
			//
			// Performs conversion from kg/m^3 to molar mass expressed in kg/mol.
			//
			// Musa Mohamma, 21-10-2019

			return input * 23.55e-3;

		}
		public static double kg_per_m3_to_mol_weight(double input)
		{
			// output = from_kg_per_m3_to_mol_weight(input)
			//
			// Performs conversion from kg/m^3 to molecular weight expressed in g/mol (lbm/lbm-mole).
			//
			// Musa Mohamma, 21-10-2019

			return input * 23.55;

		}
		public static double kg_per_m3_to_Pa_per_m(double input)
		{
			// output = from_kg_per_m3_to_Pa_per_m(input)
			//
			// Performs conversion from density in kg/m^3 to
			// gradient in Pa/m.
			//
			// Musa Mohamma, 21-10-2019

			return input * 9.80665;

		}
		public static double kg_per_m3_to_ppg(double input)
		{
			// output = from_kg_per_m3_to_ppg(input)
			//
			// Performs unit conversion from kg/m^3 to lbm/gal.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.198264e2;

		}
		public static double kg_per_m3_to_psi_per_ft(double input)
		{
			// output = from_kg_per_m3_to_psi_per_ft(input)
			//
			// Performs unit conversion from kg/m^3 to psi/ft.
			//
			// Musa Mohamma, 21-10-2019

			return input * 9.80665 / 2.262059e4;

		}
		public static double kg_to_lbm(double input)
		{
			// output = from_kg_to_lbm(input)
			//
			// Performs unit conversion from kg to lbm.
			//
			// Musa Mohamma, 21-10-2019

			return input / 4.535924e-1;

		}
		public static double km_to_mile(double input)
		{
			// output = from_km_to_mile(input)
			//
			// Performs unit conversion from km to mile.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.609344;

		}
		public static double K_to_deg_R(double input)
		{
			// output = from_K_to_deg_R(input)
			//
			// Performs unit conversion from K to degrees R (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 9.0 / 5.0;

		}
		public static double lbf_ft_to_N_m(double input)
		{
			// output = from_lbf_ft_to_N_m(input)
			//
			// Performs unit conversion from lbf*ft to N*m.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.355818;

		}
		public static double lbf_to_N(double input)
		{
			// output = from_lbf_to_N(input)
			//
			// Performs unit conversion from lbf to N.
			//
			// Musa Mohamma, 21-10-2019

			return input * 4.448222;

		}
		public static double lbm_per_ft3_to_kg_per_m3(double input)
		{
			// output = from_lbm_per_ft3_to_kg_per_m3(input)
			//
			// Performs unit conversion from lbm/ft^3 to kg/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.601846e1;

		}
		public static double lbm_per_ft3_to_Pa_per_m(double input)
		{
			// output = from_lbm_per_ft3_to_Pa_per_m(input)
			//
			// Performs unit conversion from density in lbm/ft^3 to
			// gradient in Pa/m.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.601846e1 * 9.80665;

		}
		public static double lbm_per_ft3_to_psi_per_ft(double input)
		{
			// output = from_lbm_per_ft3_to_psi_per_ft(input)
			//
			// Performs conversion from density in lbm/ft^3 to
			// gradient in psi/ft (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / 144;

		}
		public static double lbm_to_kg(double input)
		{
			// output = from_lbm_to_kg(input)
			//
			// Performs unit conversion from lbm to kg.
			//
			// Musa Mohamma, 21-10-2019
			//
			return input * 4.535924e-1;

		}
		public static double liq_grav_to_deg_API(double input)
		{
			// output = from_liq_grav_to_deg_API(input)
			//
			// Performs unit conversion from liquid gravity (wrt. water = 1000 kg/m^3) to degrees API.
			//
			// Musa Mohamma, 21-10-2019

			return 141.5e3 / (1000 * input) - 131.5;

		}
		public static double liq_grav_to_kg_per_m3(double input)
		{
			// output = from_liq_grav_to_kg_per_m3(input)
			//
			// Performs conversion from specific liquid gravity (density
			// relative to density of water at standard conditions = 999 kg/m^3 =
			// 62.4 lbm/ft^3) to kg/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input * 999.0;

		}
		public static double m2_per_d_Pa_to_bpd_psi_ft(double input)
		{
			// output = from_m2_per_d_Pa_to_bpd_psi_ft(input)
			//
			// Performs unit conversion from m^3/(d*Pa*m) = m^2/(d*Pa) to bbl/(d*psi*ft).
			//
			// Musa Mohamma, 21-10-2019

			return input / 7.565341e-5;

		}
		public static double m2_per_s_Pa_to_bpd_psi_ft(double input)
		{
			// output = from_m2_per_s_Pa_to_bpd_psi_ft(input)
			//
			// Performs unit conversion from m^3/(s*Pa*m) = m^2/(s*Pa) to bbl/(d*psi*ft).
			//
			// Musa Mohamma, 21-10-2019

			return input / 8.756182e-10;

		}
		public static double m2_per_s_to_cSt(double input)
		{
			// output = from_m2_per_s_to_cSt(input)
			//
			// Performs unit conversion from m^2/s to cSt (exact).
			//
			// Musa Mohamma, 21-10-2019
			//
			return input / 1.0e-6;

		}
		public static double m2_to_ft2(double input)
		{
			// output = from_m2_to_ft2(input)
			//
			// Performs unit conversion from m^2 to ft^2 (exact).
			//
			// Musa Mohamma, 21-10-2019
			//
			return input / ((3.048e-1) * (3.048e-1));

		}
		public static double m2_to_in2(double input)
		{
			// output = from_m2_to_in2(input)
			//
			// Performs unit conversion from m^2 to in^2 (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / ((2.54e-2) * (2.54e-2));

		}
		public static double m2_to_mD(double input)
		{
			// output = from_m2_to_mD(input)
			//
			// Performs unit conversion from m^2 to mD.
			//
			// Musa Mohamma, 21-10-2019

			return input / 9.869233e-16;

		}
		public static double m3_per_d_Pa_to_bpd_psi(double input)
		{
			// output = from_m3_per_d_Pa_to_bpd_psi(input)
			//
			// Performs unit conversion from m^3/(d*Pa) to bbl/(d*psi).
			//
			// Musa Mohamma, 21-10-2019

			return input / 2.305916e-5;

		}
		public static double m3_per_d_to_bpd(double input)
		{
			// output = from_m3_per_d_to_bpd(input)
			//
			// Performs unit conversion from m^3/d to bpd.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.589873e-1;

		}
		public static double m3_per_d_to_ft3_per_d(double input)
		{
			// output = from_m3_per_d_to_ft3_per_d(input)
			//
			// Performs unit conversion from m^3/d to ft^3/d.
			//
			// Musa Mohamma, 21-10-2019

			return input / 2.831685e-2;

		}
		public static double m3_per_m3_to_bbl_per_ft3(double input)
		{
			// output = from_m3_per_m3_to_bbl_per_ft3(input)
			//
			// Performs unit conversion from m^3/m^3 to bbl/ft^3.
			//
			// Musa Mohamma, 21-10-2019

			return input / 5.6146;

		}
		public static double m3_per_m3_to_ft3_per_bbl(double input)
		{
			// output = from_m3_per_m3_to_ft3_per_bbl(input)
			//
			// Performs unit conversion from m^3/m^3 to ft^3/bbl.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.781076e-1;

		}
		public static double m3_per_s_Pa_to_bpd_psi(double input)
		{
			// output = from_m3_per_s_Pa_to_bpd_psi(input)
			//
			// Performs unit conversion from m^3/(s*Pa) to bbl/(d*psi).
			//
			// Musa Mohamma, 21-10-2019

			return input / 2.668884e-10;

		}
		public static double m3_per_s_to_bpd(double input)
		{
			// output = from_m3_per_s_to_bpd(input)
			//
			// Performs unit conversion from m^3/s to bpd.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.840131e-6;

		}
		public static double m3_per_s_to_ft3_per_d(double input)
		{
			// output = from_m3_per_s_to_ft3_per_d(input)
			//
			// Performs unit conversion from m^3/s to ft^3/d.
			//
			// Musa Mohamma, 21-10-2019

			return input / 3.277413e-7;

		}
		public static double m3_per_s_to_ft3_per_s(double input)
		{
			// output = from_m3_per_s_to_ft3_per_s(input)
			//
			// Performs unit conversion from m^3/s to ft^3/s.
			//
			// Musa Mohamma, 21-10-2019

			return input / 2.831685e-2;

		}
		public static double m3_per_s_to_gal_per_min(double input)
		{
			// function output = from_m3_per_s_to_gal_per_min(input)
			//
			// Performs unit conversion from m^3/s to gal/min.
			//
			// Musa Mohamma, 21-10-2019

			return input / 6.309020e-5;

		}
		public static double m3_to_bbl(double input)
		{
			// output = from_m3_to_bbl(input)
			//
			// Performs unit conversion from m^3 to bbl.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.589873e-1;

		}
		public static double m3_to_ft3(double input)
		{
			// output = from_m3_to_ft3(input)
			//
			// Performs unit conversion from m^3 to ft^3 (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / ((3.048e-1) * (3.048e-1) * (3.048e-1));

		}
		public static double mD_to_m2(double input)
		{
			// output = from_mD_to_m2(input)
			//
			// Performs unit conversion from mD to m^2.
			//
			// Musa Mohamma, 21-10-2019

			return input * 9.869233e-16;

		}
		public static double mile_to_km(double input)
		{
			// output = from_mile_to_km(input)
			//
			// Performs unit conversion from mile to km.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.609344;

		}
		public static double molar_mass_to_gas_grav(double input)
		{
			// output = from_molar_mass_to_gas_grav(input)
			//
			// Performs conversion from molar mass expressed in kg/mol to specific gas gravity (density
			// relative to density of air at standard conditions = 1.23 kg/m^3 = 76.3 * 10^-3 lbm/ft^3).
			//
			// Musa Mohamma, 21-10-2019

			return input / 28.97e-3;

		}
		public static double molar_mass_to_kg_per_m3(double input)
		{
			// output = from_molar_mass_to_kg_per_m3(input)
			//
			// Performs conversion from molar mass expressed in kg/mol to kg/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input / 23.55e-3;

		}
		public static double mol_weight_to_gas_grav(double input)
		{
			// output = from_mol_weight_to_gas_grav(input)
			//
			// Performs conversion from molecular weight expressed in g/mol (lbm/lbm-mole) to specific gas
			// gravity (density relative to density of air at standard conditions = 1.23 kg/m^3 = 76.3 *
			// 10^-3 lbm/ft^3).
			//
			// Musa Mohamma, 21-10-2019

			return input / 28.97;

		}
		public static double mol_weight_to_kg_per_m3(double input)
		{
			// output = from_mol_weight_to_kg_per_m3(input)
			//
			// Performs conversion from molecular weight expressed in g/mol (lbm/lbm-mole) to kg/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input / 23.55;

		}
		public static double m_per_sec_to_ft_per_s(double input)
		{
			// output = from_m_per_sec_to_ft_per_s(input)
			//
			// Performs unit conversion from m/s to ft/s (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / 3.048e-1;

		}
		public static double m_to_ft(double input)
		{
			// output = from_m_to_ft(input)
			//
			// Performs unit conversion from m to ft (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / 3.048e-1;

		}
		public static double m_to_in(double input)
		{
			// output = from_m_to_in(input)
			//
			// Performs unit conversion from m to in (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / 0.0254;

		}
		public static double N_m_to_lbf_ft(double input)
		{
			// output = from_N_m_to_lbf_ft(input)
			//
			// Performs unit conversion from N*m to lbf*ft.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.355818;

		}
		public static double N_per_m_to_dyne_per_cm(double input)
		{
			// output = from_N_per_m_to_dyne_per_cm(input)
			//
			// Performs unit conversion from N/m to dyne/cm (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / 1e-3;

		}
		public static double N_to_lbf(double input)
		{
			// output = from_N_to_lbf(input)
			//
			// Performs unit conversion from N to lbf.
			//
			// Musa Mohamma, 21-10-2019

			return input / 4.448222;

		}
		public static double Pa_per_m_to_kg_per_m3(double input)
		{
			// output = from_Pa_per_m_to_kg_per_m3(input)
			//
			// Performs conversion from gradient in Pa/m
			// to density in kg/m^3
			//
			// Musa Mohamma, 21-10-2019

			return input / 9.80665;

		}
		public static double Pa_per_m_to_lbm_per_ft3(double input)
		{
			// output = from_Pa_per_m_to_lbm_per_ft3(input)
			//
			// Performs unit conversion from gradient in Pa/m to
			// density in lbm/ft^3.
			//
			// Musa Mohamma, 21-10-2019

			return input / (9.80665 * 1.601846e1);

		}
		public static double Pa_per_m_to_psi_per_ft(double input)
		{
			// output = from_Pa_per_m_to_psi_per_ft(input)
			//
			// Performs unit conversion from Pa/m to psi/ft.
			//
			// Musa Mohamma, 21-10-2019

			return input / 2.262059e4;

		}
		public static double Pa_s_to_cp(double input)
		{
			// output = from_Pa_s_to_cp(input)
			//
			// Performs unit conversion from Pa*s to cp (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.0e-3;

		}
		public static double Pa_to_psi(double input)
		{
			// output = from_Pa_to_psi(input)
			//
			// Performs unit conversion from Pa to psia.
			//
			// Musa Mohamma, 21-10-2019

			return input / 6.894757e3;

		}
		public static double per_Pa_to_per_psi(double input)
		{
			// output = from_per_Pa_to_per_psi(input)
			//
			// Performs unit conversion from Pa^-1 to psia^-1.
			//
			// Musa Mohamma, 21-10-2019

			return input / 1.450377e-4;

		}
		public static double per_psi_to_per_Pa(double input)
		{
			// output = from_per_psi_to_per_Pa(input)
			//
			// Performs unit conversion from Pa^-1 to psia^-1.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.450377e-4;

		}
		public static double ppg_to_kg_per_m3(double input)
		{
			// output = from_ppg_to_kg_per_m3(input)
			//
			// Performs unit conversion from lbm/gal to kg/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input * 1.198264e2;

		}
		public static double psi_per_ft_to_kg_per_m3(double input)
		{
			// output = from_psi_per_ft_to_kg_per_m3(input)
			//
			// Performs unit conversion from psi/ft to kg/m^3.
			//
			// Musa Mohamma, 21-10-2019

			return input * 2.262059e4 / 9.80665;

		}
		public static double psi_per_ft_to_lbm_per_ft3(double input)
		{
			// output = from_psi_per_ft_to_lbm_per_ft3(input)
			//
			// Performs conversion from gradient in psi/ft to
			// density in lbm/ft^3 (exact).
			//
			// Musa Mohamma, 21-10-2019

			return input * 144;

		}
		public static double psi_per_ft_to_Pa_per_m(double input)
		{
			// output = from_psi_per_ft_to_Pa_per_m(input)
			//
			// Performs unit conversion from psi/ft to Pa/m.
			//
			// Musa Mohamma, 21-10-2019

			return input * 2.262059e4;

		}
		public static double psi_to_Pa(double input)
		{
			// output = from_psi_to_Pa(input)
			//
			// Performs unit conversion from psia to Pa.
			//
			// Musa Mohamma, 21-10-2019
			//
			return input * 6.894757e3;

		}
		public static double rad_to_deg(double input)
		{
			// output = from_rad_to_deg(input)
			//
			// Performs conversion from radians to degrees.
			//
			// Musa Mohamma, 21-10-2019

			return input * 180 / Math.PI;

		}
		public static double W_to_hp(double input)
		{
			// output = from_W_to_hp(input)
			//
			// Performs unit conversion from W to hp.
			//
			// Musa Mohamma, 21-10-2019

			return input / 7.456999;

		}


	} //end namespace Units
}
