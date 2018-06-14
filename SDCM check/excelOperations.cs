﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDCM_check
{
    class excelOperations
    {
        public static double[] GetEllipseParameters(string CCT)
        {
            double A = 0;
            double B = 0;
            double C = 0;
            double D = 0;
            switch (CCT)
            {
                case "2700": //0,59201318 	(0,805928280)	0,002700 	0,001400 
                    {
                        A = 0.59201318;
                        B = -0.805928280;
                        C = 0.002700;
                        D = 0.001400;
                        break;
                    }
                case "3000": //3000	0.59879065	-0.80090559	0.00278	0.00136
                    {
                        A = 0.59879065;
                        B = -0.80090559;
                        C = 0.00278;
                        D = 0.00136;
                        break;
                    }
                case "3500": //3500K (White)	0.58778525	-0.80901699	0.00309	0.00138
                    {
                        A = 0.58778525;
                        B = -0.80901699;
                        C = 0.00309;
                        D = 0.00138;
                        break;
                    }
                case "4000": //4000K/4100K (Cool white)	0.59177872	-0.80610046	0.00313	0.00134
                    {
                        A = 0.59177872;
                        B = -0.80610046;
                        C = 0.00313;
                        D = 0.00134;
                        break;
                    }
                case "4100": //4000K/4100K (Cool white)	0.59177872	-0.80610046	0.00313	0.00134
                    {
                        A = 0.59177872;
                        B = -0.80610046;
                        C = 0.00313;
                        D = 0.00134;
                        break;
                    }
                case "5000": //5000K	0.50578285	-0.86266083	0.00274	0.00118
                    {
                        A = 0.50578285;
                        B = -0.86266083;
                        C = 0.00274;
                        D = 0.00118;
                        break;
                    }
                case "6500": //6500K (Daylight)	0.52150612	-0.85324754	0.00223	0.00095
                    {
                        A = 0.52150612;
                        B = -0.85324754;
                        C = 0.00223;
                        D = 0.00095;
                        break;
                    }
            }
            return new double[] { A, B, C, D };
        }

        public static Dictionary<string, ModelSpecification> loadExcel()
        {

            Dictionary<string, ModelSpecification> result = new Dictionary<string, ModelSpecification>();
            string FilePath = @"Y:\Manufacturing_Center\Integral Quality Management\Dane CofC.xlsx";

            if (File.Exists(FilePath))
            {
                var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var pck = new OfficeOpenXml.ExcelPackage();
                try
                {
                    pck = new OfficeOpenXml.ExcelPackage(fs);
                }
                catch (Exception e) { MessageBox.Show(e.Message); }

                if (pck.Workbook.Worksheets.Count != 0)
                {

                    //foreach (OfficeOpenXml.ExcelWorksheet worksheet in pck.Workbook.Worksheets)
                    {
                        OfficeOpenXml.ExcelWorksheet worksheet = pck.Workbook.Worksheets[1];
                        int modelColIndex = -1;
                        int sdcmColIndex = -1;
                        int cxColIndex = -1;
                        int cyColIndex = -1;
                        int cctColIndex = -1;
                        for (int col = 1; col < 11; col++)
                        {
                            if (worksheet.Cells[1, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "MODELNAME")
                            {
                                modelColIndex = col;
                            }
                            if (worksheet.Cells[1, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "MACADAM(DS.)")
                            {
                                sdcmColIndex = col;
                            }
                            if (worksheet.Cells[1, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "CIEX")
                            {
                                cxColIndex = col;
                            }
                            if (worksheet.Cells[1, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "CIEY")
                            {
                                cyColIndex = col;
                            }
                            if (worksheet.Cells[1, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "CCT(K)")
                            {
                                cctColIndex = col;
                            }
                        }


                        //Model Name	CCT(K)	IF(mA)	MacAdam (ds.)	CIEx	CIEy


                        for (int row = 2; row < worksheet.Dimension.End.Row; row++)
                        {
                            if (worksheet.Cells[row, modelColIndex].Value != null)
                            {
                                string model = worksheet.Cells[row, modelColIndex].Value.ToString().Replace(" ", "").Trim();
                                if (result.ContainsKey(model)) continue;
                                string sdcmString = worksheet.Cells[row, sdcmColIndex].Value.ToString().Replace(" ", "").Trim();
                                string cxString = worksheet.Cells[row, cxColIndex].Value.ToString().Replace(" ", "").Trim().Replace(".",",");
                                string cyString = worksheet.Cells[row, cyColIndex].Value.ToString().Replace(" ", "").Trim().Replace(".", ",");
                                string cct = ( worksheet.Cells[row, cctColIndex].Value.ToString().Replace(" ", "").Trim());
                                double[] ellipseShape = GetEllipseParameters(cct);

                                double sdcm = Convert.ToDouble(sdcmString, new CultureInfo("pl-PL"));
                                double cx = Convert.ToDouble(cxString, new CultureInfo("pl-PL"));
                                double cy = Convert.ToDouble(cyString, new CultureInfo("pl-PL"));

                                ModelSpecification newModel = new ModelSpecification(sdcm, cx, cy, ellipseShape[0], ellipseShape[1], ellipseShape[2], ellipseShape[3], 0,0,0,0,0,0,0,0);
                                result.Add(model,newModel);
                                Debug.WriteLine(model + "-" + cct + "-" + ellipseShape[0]);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Brak dostępu do pliku: Dane CoC.xlsx");
            }
            return result;
        }
    }
}
