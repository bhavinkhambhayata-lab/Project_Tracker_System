
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp;
using System.IO;

public class PrintPDFReport
{

    public static MemoryStream CreatePDF(string pdfName, string para, string strWidth, string strAlign, string ReportHeader, DataTable dt, string strHeaderName, bool bbBorder = true, bool hdBorder = true, string CompanyName = "", bool DateRequired = true, bool HtmlParse = false, bool isCmdLine = false, string strAddress = "", int VerticalAlignment = 5, bool OrntLandscap = false, bool isPrintAddress = false, int FontSize = 8, bool LeftRightStyle = false, int LeftPortionColCount = 1, int ReportHeaderAlign = Element.ALIGN_CENTER, string strReportFooter = "", string StrLocation = "L", bool HeaderNewline = true, int LineHeight = 0, bool LRBorder = false)
    {
        Document myDocument = null;
        System.IO.MemoryStream mStream = null;
        int intSpace = 0;
        if (OrntLandscap == true)
        {
            myDocument = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);
            //edited by niyati to put date at right side
            intSpace = 460;
        }
        else
        {
            myDocument = new Document(PageSize.A4, 25, 25, 25, 25);
            //intSpace = 250
            //edited by niyati to put date at right side
            intSpace = 310;
        }



        string[] arrpara = null;
        string[] arrWidth = null;
        string[] arrAlign = null;
        string[] arrHeaderName = null;
        HeaderFooter header1 = null;
        pdfName = pdfName + ".pdf";

        try
        {
            Phrase phCompanyName = new Phrase((string.IsNullOrEmpty(CompanyName) ? "" : CompanyName + "\n"), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize + 2, Font.BOLD));
            Phrase phCompanyName1 = new Phrase(("\n" + ReportHeader), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));


            Phrase phNewLine = new Phrase(Environment.NewLine + "");
            Phrase fontsize1 = new Phrase("", new Font(Font.GetFamilyIndex("Microsoft Sans Serif"), FontSize, Font.BOLD));

            PdfWriter writer = null;

            mStream = new System.IO.MemoryStream();
            writer = PdfWriter.GetInstance(myDocument, mStream);
            writer.CloseStream = false;

            //writer = PdfWriter.GetInstance(myDocument, new FileStream("D:\\temp\\" + pdfName, FileMode.Create));

            // create add the event handler 
            MyPageEvents events = new MyPageEvents();
            writer.PageEvent = events;

            header1 = new HeaderFooter(new Phrase(7.0F, ""), false);
            header1.Alignment = Element.ALIGN_CENTER;
            header1.BorderWidth = 0.5F;
            header1.Border = Rectangle.NO_BORDER;

            header1.Before.Add(0, phCompanyName);
            header1.Before.Add(1, phCompanyName1);

            if (isPrintAddress)
            {
                header1.Before.Add(1, new Phrase((string.IsNullOrEmpty(strAddress) ? " " + Environment.NewLine : Environment.NewLine + strAddress).ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize)));
            }

            header1.Alignment = Element.ALIGN_CENTER;

            myDocument.Header = header1;
            header1 = new HeaderFooter(new Phrase(7.0F, Environment.NewLine, FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD)), false);

            Font fnt = new Font(Font.GetFamilyIndex("Microsoft Sans Serif"), 6, Font.BOLD);
            if (DateRequired == true)
            {
                //header1.Before.Add(0, New Phrase((Format(Date.Now(), "dd/MMM/yyyy")).ToString + constPoweredBy.PadLeft(intSpace, " "), fnt))
                //edited by niyati to remove powered by

                header1.Before.Add(0, new Phrase((DateTime.Now.ToString("dd/MMM/yyyy").PadLeft(intSpace, ' ')).ToString(), fnt));
                //''''''''''''
            }
            
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                if (!Common.ConvertDBnullToBool(HttpContext.Current.Session["DoNotPrintPDFFooter"]))
                {
                    header1.Before.Add(0, new Phrase("Powered by www.ziperp.net", fnt));
                    header1.Alignment = Element.ALIGN_LEFT;
                }
            }
            else
            {
                string code = Common.GetIdByNameGeneralisedMethod("EnterpriseId", "Name", "Tenants", CompanyName, "", "");
                if(Common.CheckExistance("1", "DoNotPrintPDFFooter", "Enterprises", "and code = '"+ code +"'", true, false, "") <= 0 )
                {
                    header1.Before.Add(0, new Phrase("Powered by www.ziperp.net", fnt));
                    header1.Alignment = Element.ALIGN_LEFT;
                }
            }
            header1.Border = Rectangle.NO_BORDER;
            header1.BorderWidthTop = 0.5F;
            myDocument.Footer = header1;

            myDocument.Open();

            Paragraph paragraphHeader = new Paragraph(ReportHeader, FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
            string strHd;
            strHd = CompanyName + " - " + ReportHeader;
            paragraphHeader.Alignment = ReportHeaderAlign;
            //myDocument.Add(paragraphHeader);

            arrpara = para.Split('|');
            arrWidth = strWidth.Split('|');
            arrAlign = strAlign.Split('|');
            arrHeaderName = strHeaderName.Split('|');
            int colCount = dt.Columns.Count;
            int rowCount = dt.Rows.Count;

            PdfPTable tmpTable = new PdfPTable(arrHeaderName.Length);
            PdfPTable originalTable = new PdfPTable(arrHeaderName.Length);

            int[] headerWidth = new int[arrWidth.Length];

            //INSTANT C# NOTE: Commented this declaration since looping variables in 'foreach' loops are declared in the 'foreach' header in C#:
            //			string width = null;
            int j = 0;
            foreach (string width in arrWidth)
            {
                headerWidth[j] = Convert.ToInt32(width);
                j = j + 1;
            }

            int[] bodyAlign = new int[arrAlign.Length];
            int[] border = new int[arrAlign.Length];

            setAlignBorder(arrAlign, ref bodyAlign, ref border); //to be checked 



            tmpTable.SetWidths(headerWidth);
            tmpTable.WidthPercentage = 100;
            tmpTable.SpacingBefore = 3.0F;

            bool FlagHeader = true;
            if (FlagHeader == true)
            {
                for (colCount = 0; colCount < arrpara.Length; colCount++)
                {
                    Phrase fontsizeHeader = new Phrase(arrHeaderName[colCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                    //tmpTable.DefaultCell.GrayFill = 0.9F
                    if (LineHeight != 0 && !string.IsNullOrEmpty(Convert.ToString(LineHeight)))
                    {
                        tmpTable.DefaultCell.FixedHeight = LineHeight;
                    }

                    tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[colCount];
                    tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                    if (hdBorder == false)
                    {
                        tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                    }
                    else
                    {
                        if (LRBorder == true)
                        {
                            tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER; //to be checked  
                        }
                        else
                        {
                            tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked  
                        }
                    }

                    tmpTable.AddCell(fontsizeHeader);
                }

                FlagHeader = false;
            }


            tmpTable.HeaderRows = 1;

            //tmpTable.DefaultCell.GrayFill = 1.0F
            tmpTable.DefaultCell.VerticalAlignment = 5;
            string str = "";
            bool isBold = false;
            bool isItalic = false;
            Font fnt11 = new Font();
            Font fnt12 = new Font();
            string[] LinePara = null;
            bool PrintLine = false;
            bool tMiddle = false;
            bool tNewPage = false;

            if (HtmlParse == false)
            {
                long tSrNo = 0;
                tSrNo = 1;
                foreach (DataRow row in dt.Rows)
                {
                    PrintLine = true;
                    colCount = 0;
                    isBold = false;
                    isItalic = false;
                    tMiddle = false;
                    tNewPage = false;

                    if (LeftRightStyle)
                    {
                        if (!(dt.Columns.Contains("_LeftStyle")))
                        {
                            fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {

                            if (string.IsNullOrEmpty(("" + row["_LeftStyle"])))
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_LeftStyle"]) == "B")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_LeftStyle"]) == "I")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_LeftStyle"]) == "BI")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                        }
                        if (!(dt.Columns.Contains("_RightStyle")))
                        {
                            fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(("" + row["_RightStyle"])))
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_RightStyle"]) == "B")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_RightStyle"]) == "I")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_RightStyle"]) == "BI")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                        }

                    }
                    else
                    {

                        if (!(dt.Columns.Contains("_Style")))
                        {
                            fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(("" + row["_Style"])))
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_Style"]) == "B")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_Style"]) == "I")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_Style"]) == "BI")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                            if (("" + row["_Style"]) == "H")
                            {
                                PrintLine = false;
                            }
                            if (("" + row["_Style"]) == "M")
                            {
                                tMiddle = true;
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.BOLD);
                            }
                            if (("" + row["_Style"]) == "MN")
                            {
                                tMiddle = true;
                                tNewPage = true;
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.BOLD + Font.UNDERLINE);
                            }
                        }
                    }


                    if (!(dt.Columns.Contains("cmdLine")))
                    {
                        LinePara = row["cmdLine"].ToString().Split('|');
                    }
                    else
                    {

                        LinePara = row["cmdLine"].ToString().Split('|');

                    }
                    if (PrintLine)
                    {



                        foreach (var str1 in arrpara)
                        {

                            //Dim helvetica As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, True)
                            //Dim fnt11 As New Font(helvetica, 9, Font.NORMAL)
                            Phrase phCell = null;
                            if (str1 == "_SrNo")
                            {
                                phCell = new Phrase(tSrNo.ToString(), fnt11);
                            }
                            else
                            {
                                if (LeftRightStyle)
                                {
                                    if (colCount <= LeftPortionColCount)
                                    {
                                        phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt11);
                                    }
                                    else
                                    {
                                        phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt12);
                                    }

                                }
                                else
                                {
                                    phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt11);
                                }

                            }

                            //Dim phCell As New Phrase(row(arrpara(colCount).ToString()).ToString, fnt11)

                            //tmpTable.DefaultCell.FixedHeight = 15
                            tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[colCount]; //to be checked 
                            tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                            //tmpTable.DefaultCell.VerticalAlignment = VerticalAlignment;
                            if (bbBorder == false)
                            {

                                tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked 
                            }
                            else
                            {
                                if (isCmdLine == true && LRBorder == true)
                                {
                                    if (LinePara[0] != "NLR")
                                    {
                                        tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER; //to be checked  
                                    }
                                    else
                                    {
                                        tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                                    }
                                }
                                else
                                {
                                    tmpTable.DefaultCell.Border = Rectangle.NO_BORDER;  //to be checked  
                                }
                            }


                            if (isCmdLine == true)
                            {
                                if (colCount <= LinePara.Length - 1)
                                {
                                    if (LinePara[colCount] == "A")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.TOP_BORDER;
                                    }
                                    else if (LinePara[colCount] == "B")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER;
                                    }
                                    else if (LinePara[colCount] == "AB")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER;
                                    }
                                }
                                else
                                {
                                    if (LinePara[LinePara.Length - 1] == "A")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.TOP_BORDER;
                                    }
                                    else if (LinePara[LinePara.Length - 1] == "B")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER;
                                    }
                                    else if (LinePara[LinePara.Length - 1] == "AB")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER;
                                    }
                                }

                            }
                            if (tMiddle && colCount == 0)
                            {
                                if (tNewPage == true)
                                {
                                    if (tSrNo != 1)
                                    {
                                        myDocument.Add(tmpTable);
                                        myDocument.NewPage();
                                        tmpTable = new PdfPTable(arrHeaderName.Length);
                                        tmpTable.SetWidths(headerWidth);
                                        tmpTable.WidthPercentage = 100;
                                        tmpTable.SpacingBefore = 3.0F;
                                        //  myDocument.Add(paragraphHeader);

                                        if (HeaderNewline == true)
                                        {
                                            for (int ttcolCount = 0; ttcolCount < arrpara.Length; ttcolCount++)
                                            {
                                                Phrase fontsizeHeader = new Phrase(arrHeaderName[ttcolCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                                                tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[ttcolCount];
                                                tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                                                if (hdBorder == false)
                                                {
                                                    //tmpTable.DefaultCell.Border = Rectangle.NO_BORDER + (BorderStyle)border[ttcolCount]; //to be checked 
                                                }
                                                else
                                                {
                                                    tmpTable.DefaultCell.Border = Rectangle.TOP_BORDER;
                                                }

                                                tmpTable.AddCell(fontsizeHeader);
                                            }
                                        }

                                    }
                                }
                                tmpTable.DefaultCell.Colspan = arrpara.Length;
                                tmpTable.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                tmpTable.DefaultCell.Border = Rectangle.NO_BORDER;
                            }
                            if (!tMiddle)
                            {
                                tmpTable.AddCell(phCell);
                            }
                            else
                            {
                                if (colCount == 0)
                                {
                                    tmpTable.AddCell(phCell);
                                }
                            }

                            //If str <> "_SrNo" Then
                            colCount += 1;
                            //End If

                        }
                        if (tMiddle)
                        {
                            tmpTable.DefaultCell.Colspan = 1;
                        }



                        tSrNo = tSrNo + 1;
                    }
                }
                //Next;
            }
            else
            {

            }

            myDocument.Add(tmpTable);
            if (strReportFooter != "")
            {
                Phrase PhReportFooter = new Phrase(strReportFooter, FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                myDocument.Add(PhReportFooter);
            }
            return mStream;
        }
        catch (DocumentException de)
        {
            throw de;


        }
        catch (IOException ioe)
        {
            throw ioe;
        }
        catch (Exception EX)
        {
            throw EX;

        }
        finally
        {
            myDocument.Close();
        }

    }


    public static void setAlignBorder(string[] arrAlign, ref int[] bodyAlign, ref int[] border)
    {
        //INSTANT C# NOTE: Commented this declaration since looping variables in 'foreach' loops are declared in the 'foreach' header in C#:
        //		string align = null;
        int j = 0;
        foreach (string align in arrAlign)
        {
            if (align.IndexOf("<") + 1 > 0)
            {
                bodyAlign[j] = Element.ALIGN_LEFT;
            }
            else if (align.IndexOf(">") + 1 != 0)
            {
                bodyAlign[j] = Element.ALIGN_RIGHT;
            }
            else if ((align.IndexOf("^") + 1) != 0)
            {
                bodyAlign[j] = Element.ALIGN_CENTER;
            }

            if (align.Length == 1)
            {
                border[j] = Rectangle.NO_BORDER;
            }
            else if (align.Length == 2)
            {
                if (align[0] == '!')
                {
                    border[j] = Rectangle.LEFT_BORDER;
                }
                else
                {
                    border[j] = Rectangle.RIGHT_BORDER;
                }
            }
            else if (align.Length == 3)
            {
                border[j] = Rectangle.RIGHT_BORDER + Rectangle.LEFT_BORDER;
            }
            j = j + 1;
        }
    }


    public static MemoryStream CreatePDF_LedgerConfirmation(string pdfName, string para, string strWidth, string strAlign, string ReportHeader, DataTable dt, string strHeaderName, bool bbBorder = true, bool hdBorder = true, string CompanyName = "", bool DateRequired = true, bool HtmlParse = false, bool isCmdLine = false, string strAddress = "", string AccountName = "", string AccountAdderess = "", string CompanyAddress = "", string Subject = "", string Content = "", int VerticalAlignment = 5, bool OrntLandscap = false, bool isPrintAddress = false, int FontSize = 8, bool LeftRightStyle = false, int LeftPortionColCount = 1, int ReportHeaderAlign = Element.ALIGN_CENTER, string strReportFooter = "", string StrLocation = "L", bool HeaderNewline = true, int LineHeight = 0)
    {
        Document myDocument = null;
        System.IO.MemoryStream mStream = null;
        int intSpace = 0;
        if (OrntLandscap == true)
        {
            myDocument = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);
            intSpace = 460;
        }
        else
        {
            myDocument = new Document(PageSize.A4, 25, 25, 25, 25);
            intSpace = 310;
        }



        string[] arrpara = null;
        string[] arrWidth = null;
        string[] arrAlign = null;
        string[] arrHeaderName = null;
        HeaderFooter header1 = null;
        pdfName = pdfName + ".pdf";

        try
        {
            Phrase phCompanyName = new Phrase((string.IsNullOrEmpty(CompanyName) ? "" : CompanyName + "\n"), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize + 2, Font.BOLD));
            Phrase phCompanyName1 = new Phrase(("\n" + ReportHeader), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));


            Phrase phNewLine = new Phrase(Environment.NewLine + "");
            Phrase fontsize1 = new Phrase("", new Font(Font.GetFamilyIndex("Microsoft Sans Serif"), FontSize, Font.BOLD));

            PdfWriter writer = null;

            mStream = new System.IO.MemoryStream();
            writer = PdfWriter.GetInstance(myDocument, mStream);
            writer.CloseStream = false;

            //writer = PdfWriter.GetInstance(myDocument, new FileStream("D:\\temp\\" + pdfName, FileMode.Create));
            // create add the event handler 
            MyPageEvents events = new MyPageEvents();
            writer.PageEvent = events;
            Font fnt = new Font(Font.GetFamilyIndex("Microsoft Sans Serif"), 12, Font.BOLD);

            header1 = new HeaderFooter(new Phrase(7.0F, ""), false);
            header1.Alignment = Element.ALIGN_CENTER;
            header1.BorderWidth = 0.5F;
            header1.Border = Rectangle.NO_BORDER;

            header1.Before.Add(0, phNewLine);
            header1.Alignment = Element.ALIGN_LEFT;
            header1.Border = Rectangle.TOP_BORDER;
            header1.BorderWidthTop = 0.5F;

            myDocument.Footer = header1;

            header1 = new HeaderFooter(new Phrase(7.0F, Environment.NewLine, fnt), false);
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                if (!Common.ConvertDBnullToBool(HttpContext.Current.Session["DoNotPrintPDFFooter"]))
                {
                    header1.Before.Add(0, new Phrase("Powered by www.ziperp.net", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL)));
                    header1.Alignment = Element.ALIGN_LEFT;
                }
            }
            else
            {
                string code = Common.GetIdByNameGeneralisedMethod("EnterpriseId", "Name", "Tenants", CompanyName, "", "");
                if (Common.CheckExistance("1", "DoNotPrintPDFFooter", "Enterprises", "and code = '" + code + "'", true, false, "") <= 0)
                {
                    header1.Before.Add(0, new Phrase("Powered by www.ziperp.net", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL)));
                    header1.Alignment = Element.ALIGN_LEFT;
                }
            }
            header1.Border = Rectangle.NO_BORDER;
            header1.BorderWidthTop = 0.5F;
            myDocument.Footer = header1;

            myDocument.Open();

            arrpara = para.Split('|');
            arrWidth = strWidth.Split('|');
            arrAlign = strAlign.Split('|');
            arrHeaderName = strHeaderName.Split('|');
            int colCount = dt.Columns.Count;
            int rowCount = dt.Rows.Count;

            PdfPTable tmpTable = new PdfPTable(arrHeaderName.Length);
            PdfPTable originalTable = new PdfPTable(arrHeaderName.Length);

            int[] headerWidth = new int[arrWidth.Length];
            Chunk ChunkTitle;
            Phrase NPhrase;
            Cell HCell;

            Table htbl;
            htbl = new Table(6);

            headerWidth[0] = 5;
            headerWidth[1] = 1;
            headerWidth[2] = 44;
            headerWidth[3] = 5;
            headerWidth[4] = 1;
            headerWidth[5] = 44;

            htbl.SetWidths(headerWidth);
            htbl.WidthPercentage = 100;
            htbl.Border = Rectangle.NO_BORDER;
            htbl.Cellpadding = 2.0F;


            ChunkTitle = new Chunk("To", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(":", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(AccountName, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("From", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(":", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(CompanyName, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(" ", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(AccountAdderess, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(" ", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);


            ChunkTitle = new Chunk(CompanyAddress, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.Colspan = 6;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("Dear Sir/Madam,", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            HCell.Colspan = 3;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("Dated: " + ((DateTime.Now.ToString("dd/MMM/yyyy").PadLeft(intSpace, ' ')).ToString()), FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            HCell.Colspan = 3;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(Subject, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 9, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_CENTER;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            HCell.Colspan = 6;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(ReportHeader, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, FontSize, Font.UNDERLINE));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_CENTER;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            HCell.Colspan = 6;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.Colspan = 6;
            htbl.AddCell(HCell);



            ChunkTitle = new Chunk(Content, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.Colspan = 6;
            htbl.AddCell(HCell);



            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.Colspan = 6;
            htbl.AddCell(HCell);

            myDocument.Add(htbl);


            int j = 0;
            foreach (string width in arrWidth)
            {
                headerWidth[j] = Convert.ToInt32(width);
                j = j + 1;
            }

            int[] bodyAlign = new int[arrAlign.Length];
            int[] border = new int[arrAlign.Length];


            setAlignBorder(arrAlign, ref bodyAlign, ref border); //to be checked 


            tmpTable.SetWidths(headerWidth);
            tmpTable.WidthPercentage = 100;
            tmpTable.SpacingBefore = 3.0F;

            bool FlagHeader = true;
            if (FlagHeader == true)
            {
                for (colCount = 0; colCount < arrpara.Length; colCount++)
                {
                    Phrase fontsizeHeader = new Phrase(arrHeaderName[colCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                    //tmpTable.DefaultCell.GrayFill = 0.9F
                    if (LineHeight != 0 && !string.IsNullOrEmpty(Convert.ToString(LineHeight)))
                    {
                        tmpTable.DefaultCell.FixedHeight = LineHeight;
                    }

                    tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[colCount];
                    tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                    if (hdBorder == false)
                    {
                        tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                    }
                    else
                    {
                        tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked  
                    }

                    tmpTable.AddCell(fontsizeHeader);
                }

                FlagHeader = false;
            }


            tmpTable.HeaderRows = 1;

            //tmpTable.DefaultCell.GrayFill = 1.0F
            tmpTable.DefaultCell.VerticalAlignment = 5;
            string str = "";
            bool isBold = false;
            bool isItalic = false;
            Font fnt11 = new Font();
            Font fnt12 = new Font();
            string[] LinePara = null;
            bool PrintLine = false;
            bool tMiddle = false;
            bool tNewPage = false;

            if (HtmlParse == false)
            {
                long tSrNo = 0;
                tSrNo = 1;
                foreach (DataRow row in dt.Rows)
                {
                    PrintLine = true;
                    colCount = 0;
                    isBold = false;
                    isItalic = false;
                    tMiddle = false;
                    tNewPage = false;

                    if (LeftRightStyle)
                    {
                        if (!(dt.Columns.Contains("_LeftStyle")))
                        {
                            fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {

                            if (string.IsNullOrEmpty(("" + row["_LeftStyle"])))
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_LeftStyle"]) == "B")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_LeftStyle"]) == "I")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_LeftStyle"]) == "BI")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                        }
                        if (!(dt.Columns.Contains("_RightStyle")))
                        {
                            fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(("" + row["_RightStyle"])))
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_RightStyle"]) == "B")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_RightStyle"]) == "I")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_RightStyle"]) == "BI")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                        }

                    }
                    else
                    {

                        if (!(dt.Columns.Contains("_Style")))
                        {
                            fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(("" + row["_Style"])))
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_Style"]) == "B")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_Style"]) == "I")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_Style"]) == "BI")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                            if (("" + row["_Style"]) == "H")
                            {
                                PrintLine = false;
                            }
                            if (("" + row["_Style"]) == "M")
                            {
                                tMiddle = true;
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.BOLD);
                            }
                            if (("" + row["_Style"]) == "MN")
                            {
                                tMiddle = true;
                                tNewPage = true;
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.BOLD + Font.UNDERLINE);
                            }
                        }
                    }


                    if (!(dt.Columns.Contains("cmdLine")))
                    {
                        LinePara = row["cmdLine"].ToString().Split('|');
                    }
                    else
                    {

                        LinePara = row["cmdLine"].ToString().Split('|');

                    }
                    if (PrintLine)
                    {



                        foreach (var str1 in arrpara)
                        {

                            //Dim helvetica As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, True)
                            //Dim fnt11 As New Font(helvetica, 9, Font.NORMAL)
                            Phrase phCell = null;
                            if (str1 == "_SrNo")
                            {
                                phCell = new Phrase(tSrNo.ToString(), fnt11);
                            }
                            else
                            {
                                if (LeftRightStyle)
                                {
                                    if (colCount <= LeftPortionColCount)
                                    {
                                        phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt11);
                                    }
                                    else
                                    {
                                        phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt12);
                                    }

                                }
                                else
                                {
                                    phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt11);
                                }

                            }

                            //Dim phCell As New Phrase(row(arrpara(colCount).ToString()).ToString, fnt11)

                            //tmpTable.DefaultCell.FixedHeight = 15
                            tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[colCount]; //to be checked 
                            tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                            //tmpTable.DefaultCell.VerticalAlignment = VerticalAlignment;
                            if (bbBorder == false)
                            {
                                tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked 
                            }
                            else
                            {
                                tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked
                            }


                            if (isCmdLine == true)
                            {
                                if (colCount <= LinePara.Length - 1)
                                {
                                    if (LinePara[colCount] == "A")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.TOP_BORDER;
                                    }
                                    else if (LinePara[colCount] == "B")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER;
                                    }
                                    else if (LinePara[colCount] == "AB")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER;
                                    }
                                }
                                else
                                {
                                    if (LinePara[LinePara.Length - 1] == "A")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.TOP_BORDER;
                                    }
                                    else if (LinePara[LinePara.Length - 1] == "B")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER;
                                    }
                                    else if (LinePara[LinePara.Length - 1] == "AB")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER;
                                    }
                                }

                            }
                            if (tMiddle && colCount == 0)
                            {
                                if (tNewPage == true)
                                {
                                    if (tSrNo != 1)
                                    {
                                        myDocument.Add(tmpTable);
                                        myDocument.NewPage();
                                        tmpTable = new PdfPTable(arrHeaderName.Length);
                                        tmpTable.SetWidths(headerWidth);
                                        tmpTable.WidthPercentage = 100;
                                        tmpTable.SpacingBefore = 3.0F;
                                        //myDocument.Add(paragraphHeader)

                                        if (HeaderNewline == true)
                                        {
                                            for (int ttcolCount = 0; ttcolCount < arrpara.Length; ttcolCount++)
                                            {
                                                Phrase fontsizeHeader = new Phrase(arrHeaderName[ttcolCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                                                tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[ttcolCount];
                                                tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                                                if (hdBorder == false)
                                                {
                                                    //tmpTable.DefaultCell.Border = Rectangle.NO_BORDER + (BorderStyle)border[ttcolCount]; //to be checked 
                                                }
                                                else
                                                {
                                                    //tmpTable.DefaultCell.Border = ((BorderStyle)border(ttcolCount)) + Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked- done
                                                }

                                                tmpTable.AddCell(fontsizeHeader);
                                            }
                                        }

                                    }
                                }
                                tmpTable.DefaultCell.Colspan = arrpara.Length;
                                tmpTable.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                tmpTable.DefaultCell.Border = Rectangle.NO_BORDER;
                            }
                            if (!tMiddle)
                            {
                                tmpTable.AddCell(phCell);
                            }
                            else
                            {
                                if (colCount == 0)
                                {
                                    tmpTable.AddCell(phCell);
                                }
                            }

                            //If str <> "_SrNo" Then
                            colCount += 1;
                            //End If

                        }
                        if (tMiddle)
                        {
                            tmpTable.DefaultCell.Colspan = 1;
                        }



                        tSrNo = tSrNo + 1;
                    }
                }
                //Next;
            }
            else
            {
            }
            myDocument.Add(tmpTable);

            Table FooterTable = new Table(4);
            Cell FooterCell = new Cell();

            int[] intFooterWidth = new int[4];
            intFooterWidth[0] = 10;
            intFooterWidth[1] = 40;
            intFooterWidth[2] = 10;
            intFooterWidth[3] = 40;

            FooterTable.SetWidths(intFooterWidth);
            FooterTable.WidthPercentage = 100;
            FooterTable.Border = Rectangle.NO_BORDER;
            FooterTable.Cellpadding = 1f;

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 15f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Colspan = 4;
            FooterCell.Rowspan = 3;
            FooterCell.Border = Rectangle.TOP_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_LEFT;
            FooterCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            FooterTable.AddCell(FooterCell);

            ChunkTitle = new Chunk("I/We hereby confirm the above", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 7.5f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Colspan = 2;
            FooterCell.Border = Rectangle.NO_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_LEFT;
            FooterCell.VerticalAlignment = Element.ALIGN_TOP;
            FooterTable.AddCell(FooterCell);

            ChunkTitle = new Chunk("Yours Faithfully,", FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 7.5f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Colspan = 2;
            FooterCell.Border = Rectangle.NO_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            FooterCell.VerticalAlignment = Element.ALIGN_TOP;
            FooterTable.AddCell(FooterCell);

            //I.T. PAN NO. :
            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 50f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Border = Rectangle.NO_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_LEFT;
            FooterCell.VerticalAlignment = Element.ALIGN_TOP;
            FooterTable.AddCell(FooterCell);

            //& ToDt.Rows(0).Item("PanNo").ToString
            ChunkTitle = new Chunk(" ", FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 50f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Border = Rectangle.NO_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_LEFT;
            FooterCell.VerticalAlignment = Element.ALIGN_TOP;
            FooterTable.AddCell(FooterCell);

            myDocument.Add(FooterTable);


            if (strReportFooter != "")
            {
                Phrase PhReportFooter = new Phrase(strReportFooter, FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                myDocument.Add(PhReportFooter);
            }
            return mStream;
        }
        catch (DocumentException de)
        {
            throw de;


        }
        catch (IOException ioe)
        {
            throw ioe;
        }
        catch (Exception EX)
        {
            throw EX;

        }
        finally
        {
            myDocument.Close();
        }

    }


    public static MemoryStream CreatePDF_LedgerConfirmationWithCols(int Cols, string pdfName, string para, string strWidth, string strAlign, string ReportHeader, DataTable dt, string strHeaderName, bool bbBorder = true, bool hdBorder = true, string CompanyName = "", bool DateRequired = true, bool HtmlParse = false, bool isCmdLine = false, string strAddress = "", string AccountName = "", string AccountAdderess = "", string CompanyAddress = "", string Subject = "", string Content = "", int VerticalAlignment = 5, bool OrntLandscap = false, bool isPrintAddress = false, int FontSize = 8, bool LeftRightStyle = false, int LeftPortionColCount = 1, int ReportHeaderAlign = Element.ALIGN_CENTER, string strReportFooter = "", string StrLocation = "L", bool HeaderNewline = true, int LineHeight = 0)
    {
        Document myDocument = null;
        System.IO.MemoryStream mStream = null;
        int intSpace = 0;
        if (OrntLandscap == true)
        {
            myDocument = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);
            intSpace = 460;
        }
        else
        {
            myDocument = new Document(PageSize.A4, 25, 25, 25, 25);
            intSpace = 310;
        }



        string[] arrpara = null;
        string[] arrWidth = null;
        string[] arrAlign = null;
        string[] arrHeaderName = null;
        HeaderFooter header1 = null;
        pdfName = pdfName + ".pdf";

        try
        {
            Phrase phCompanyName = new Phrase((string.IsNullOrEmpty(CompanyName) ? "" : CompanyName + "\n"), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize + 2, Font.BOLD));
            Phrase phCompanyName1 = new Phrase(("\n" + ReportHeader), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));


            Phrase phNewLine = new Phrase(Environment.NewLine + "");
            Phrase fontsize1 = new Phrase("", new Font(Font.GetFamilyIndex("Microsoft Sans Serif"), FontSize, Font.BOLD));

            PdfWriter writer = null;

            mStream = new System.IO.MemoryStream();
            writer = PdfWriter.GetInstance(myDocument, mStream);
            writer.CloseStream = false;

            //writer = PdfWriter.GetInstance(myDocument, new FileStream("D:\\temp\\" + pdfName, FileMode.Create));
            // create add the event handler 
            MyPageEvents events = new MyPageEvents();
            writer.PageEvent = events;
            Font fnt = new Font(Font.GetFamilyIndex("Microsoft Sans Serif"), 12, Font.BOLD);

            header1 = new HeaderFooter(new Phrase(7.0F, ""), false);
            header1.Alignment = Element.ALIGN_CENTER;
            header1.BorderWidth = 0.5F;
            header1.Border = Rectangle.NO_BORDER;

            header1.Before.Add(0, phNewLine);
            header1.Alignment = Element.ALIGN_LEFT;
            header1.Border = Rectangle.TOP_BORDER;
            header1.BorderWidthTop = 0.5F;

            myDocument.Footer = header1;

            header1 = new HeaderFooter(new Phrase(7.0F, Environment.NewLine, fnt), false);
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                if (!Common.ConvertDBnullToBool(HttpContext.Current.Session["DoNotPrintPDFFooter"]))
                {
                    header1.Before.Add(0, new Phrase("Powered by www.ziperp.net", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL)));
                    header1.Alignment = Element.ALIGN_LEFT;
                }
            }
            else
            {
                string code = Common.GetIdByNameGeneralisedMethod("EnterpriseId", "Name", "Tenants", CompanyName, "", "");
                if (Common.CheckExistance("1", "DoNotPrintPDFFooter", "Enterprises", "and code = '" + code + "'", true, false, "") <= 0)
                {
                    header1.Before.Add(0, new Phrase("Powered by www.ziperp.net", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL)));
                    header1.Alignment = Element.ALIGN_LEFT;
                }
            }
            header1.Border = Rectangle.NO_BORDER;
            header1.BorderWidthTop = 0.5F;
            myDocument.Footer = header1;

            myDocument.Open();

            arrpara = para.Split('|');
            arrWidth = strWidth.Split('|');
            arrAlign = strAlign.Split('|');
            arrHeaderName = strHeaderName.Split('|');
            int colCount = dt.Columns.Count;
            int rowCount = dt.Rows.Count;

            PdfPTable tmpTable = new PdfPTable(arrHeaderName.Length);
            PdfPTable originalTable = new PdfPTable(arrHeaderName.Length);

            int[] headerWidth = new int[arrWidth.Length];
            Chunk ChunkTitle;
            Phrase NPhrase;
            Cell HCell;

            Table htbl;
            htbl = new Table(Cols);

            if (Cols > 7)
            {
                headerWidth[0] = 5;
                headerWidth[1] = 1;
                headerWidth[2] = 44;
                headerWidth[3] = 5;
                headerWidth[4] = 1;
                headerWidth[5] = 44;
                headerWidth[6] = 0;
                headerWidth[7] = 0;

            }

            else
            {
                headerWidth[0] = 5;
                headerWidth[1] = 1;
                headerWidth[2] = 44;
                headerWidth[3] = 5;
                headerWidth[4] = 1;
                headerWidth[5] = 44;
            }

            htbl.SetWidths(headerWidth);
            htbl.WidthPercentage = 100;
            htbl.Border = Rectangle.NO_BORDER;
            htbl.Cellpadding = 2.0F;


            ChunkTitle = new Chunk("To", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(":", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(AccountName, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("From", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(":", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(CompanyName, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            if (Cols > 7)
            {
                ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
                NPhrase = new Phrase(ChunkTitle);

                HCell = new Cell(NPhrase);
                HCell.Leading = 7.0F;
                HCell.Border = Rectangle.NO_BORDER;
                HCell.HorizontalAlignment = Element.ALIGN_LEFT;
                HCell.VerticalAlignment = Element.ALIGN_TOP;

                htbl.AddCell(HCell);
            }

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(" ", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(AccountAdderess, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk(" ", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);


            ChunkTitle = new Chunk(CompanyAddress, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;

            htbl.AddCell(HCell);

            if (Cols > 7)
            {
                ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
                NPhrase = new Phrase(ChunkTitle);

                HCell = new Cell(NPhrase);
                HCell.Leading = 7.0F;
                HCell.Border = Rectangle.NO_BORDER;
                HCell.HorizontalAlignment = Element.ALIGN_LEFT;
                HCell.VerticalAlignment = Element.ALIGN_TOP;

                htbl.AddCell(HCell);
            }

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.Colspan = Cols;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("Dear Sir/Madam,", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_LEFT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            HCell.Colspan = Cols / 2;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("Dated: " + ((DateTime.Now.ToString("dd/MMM/yyyy").PadLeft(intSpace, ' ')).ToString()), FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            HCell.Colspan = Cols / 2;
            htbl.AddCell(HCell);

            if (Cols == 7)
            {
                ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.BOLD));
                NPhrase = new Phrase(ChunkTitle);

                HCell = new Cell(NPhrase);
                HCell.Leading = 7.0F;
                HCell.Border = Rectangle.NO_BORDER;
                HCell.HorizontalAlignment = Element.ALIGN_LEFT;
                HCell.VerticalAlignment = Element.ALIGN_TOP;

                htbl.AddCell(HCell);
            }

            ChunkTitle = new Chunk(Subject, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 9, Font.BOLD));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_CENTER;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            HCell.Colspan = Cols;
            htbl.AddCell(HCell);


            ChunkTitle = new Chunk(ReportHeader, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, FontSize, Font.UNDERLINE));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.HorizontalAlignment = Element.ALIGN_CENTER;
            HCell.VerticalAlignment = Element.ALIGN_TOP;
            HCell.Colspan = Cols;
            htbl.AddCell(HCell);

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.Colspan = Cols;
            htbl.AddCell(HCell);



            ChunkTitle = new Chunk(Content, FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.Colspan = Cols;
            htbl.AddCell(HCell);



            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);

            HCell = new Cell(NPhrase);
            HCell.Leading = 7.0F;
            HCell.Border = Rectangle.NO_BORDER;
            HCell.Colspan = Cols;
            htbl.AddCell(HCell);

            myDocument.Add(htbl);

            int[] bodyAlign = new int[arrAlign.Length];
            int[] border = new int[arrAlign.Length];


            setAlignBorder(arrAlign, ref bodyAlign, ref border); //to be checked 


            int j = 0;
            foreach (string width in arrWidth)
            {
                headerWidth[j] = Convert.ToInt32(width);
                j = j + 1;
            }

            tmpTable.SetWidths(headerWidth);
            tmpTable.WidthPercentage = 100;
            tmpTable.SpacingBefore = 3.0F;

            bool FlagHeader = true;
            if (FlagHeader == true)
            {
                for (colCount = 0; colCount < arrpara.Length; colCount++)
                {
                    Phrase fontsizeHeader = new Phrase(arrHeaderName[colCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                    //tmpTable.DefaultCell.GrayFill = 0.9F
                    if (LineHeight != 0 && !string.IsNullOrEmpty(Convert.ToString(LineHeight)))
                    {
                        tmpTable.DefaultCell.FixedHeight = LineHeight;
                    }

                    tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[colCount];
                    tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                    if (hdBorder == false)
                    {
                        tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                    }
                    else
                    {
                        tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked  
                    }

                    tmpTable.AddCell(fontsizeHeader);
                }

                FlagHeader = false;
            }


            tmpTable.HeaderRows = 1;

            //tmpTable.DefaultCell.GrayFill = 1.0F
            tmpTable.DefaultCell.VerticalAlignment = 5;
            string str = "";
            bool isBold = false;
            bool isItalic = false;
            Font fnt11 = new Font();
            Font fnt12 = new Font();
            string[] LinePara = null;
            bool PrintLine = false;
            bool tMiddle = false;
            bool tNewPage = false;

            if (HtmlParse == false)
            {
                long tSrNo = 0;
                tSrNo = 1;
                foreach (DataRow row in dt.Rows)
                {
                    PrintLine = true;
                    colCount = 0;
                    isBold = false;
                    isItalic = false;
                    tMiddle = false;
                    tNewPage = false;

                    if (LeftRightStyle)
                    {
                        if (!(dt.Columns.Contains("_LeftStyle")))
                        {
                            fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {

                            if (string.IsNullOrEmpty(("" + row["_LeftStyle"])))
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_LeftStyle"]) == "B")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_LeftStyle"]) == "I")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_LeftStyle"]) == "BI")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                        }
                        if (!(dt.Columns.Contains("_RightStyle")))
                        {
                            fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(("" + row["_RightStyle"])))
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_RightStyle"]) == "B")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_RightStyle"]) == "I")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_RightStyle"]) == "BI")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                        }

                    }
                    else
                    {

                        if (!(dt.Columns.Contains("_Style")))
                        {
                            fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(("" + row["_Style"])))
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_Style"]) == "B")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_Style"]) == "I")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_Style"]) == "BI")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                            if (("" + row["_Style"]) == "H")
                            {
                                PrintLine = false;
                            }
                            if (("" + row["_Style"]) == "M")
                            {
                                tMiddle = true;
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.BOLD);
                            }
                            if (("" + row["_Style"]) == "MN")
                            {
                                tMiddle = true;
                                tNewPage = true;
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.BOLD + Font.UNDERLINE);
                            }
                        }
                    }


                    if (!(dt.Columns.Contains("cmdLine")))
                    {
                        LinePara = row["cmdLine"].ToString().Split('|');
                    }
                    else
                    {

                        LinePara = row["cmdLine"].ToString().Split('|');

                    }
                    if (PrintLine)
                    {



                        foreach (var str1 in arrpara)
                        {

                            //Dim helvetica As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, True)
                            //Dim fnt11 As New Font(helvetica, 9, Font.NORMAL)
                            Phrase phCell = null;
                            if (str1 == "_SrNo")
                            {
                                phCell = new Phrase(tSrNo.ToString(), fnt11);
                            }
                            else
                            {
                                if (LeftRightStyle)
                                {
                                    if (colCount <= LeftPortionColCount)
                                    {
                                        phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt11);
                                    }
                                    else
                                    {
                                        phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt12);
                                    }

                                }
                                else
                                {
                                    phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt11);
                                }

                            }

                            //Dim phCell As New Phrase(row(arrpara(colCount).ToString()).ToString, fnt11)

                            //tmpTable.DefaultCell.FixedHeight = 15
                            tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[colCount]; //to be checked 
                            tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                            //tmpTable.DefaultCell.VerticalAlignment = VerticalAlignment;
                            if (bbBorder == false)
                            {
                                tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked 
                            }
                            else
                            {
                                tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked
                            }


                            if (isCmdLine == true)
                            {
                                if (colCount <= LinePara.Length - 1)
                                {
                                    if (LinePara[colCount] == "A")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.TOP_BORDER;
                                    }
                                    else if (LinePara[colCount] == "B")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER;
                                    }
                                    else if (LinePara[colCount] == "AB")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER;
                                    }
                                }
                                else
                                {
                                    if (LinePara[LinePara.Length - 1] == "A")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.TOP_BORDER;
                                    }
                                    else if (LinePara[LinePara.Length - 1] == "B")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER;
                                    }
                                    else if (LinePara[LinePara.Length - 1] == "AB")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER;
                                    }
                                }

                            }
                            if (tMiddle && colCount == 0)
                            {
                                if (tNewPage == true)
                                {
                                    if (tSrNo != 1)
                                    {
                                        myDocument.Add(tmpTable);
                                        myDocument.NewPage();
                                        tmpTable = new PdfPTable(arrHeaderName.Length);
                                        tmpTable.SetWidths(headerWidth);
                                        tmpTable.WidthPercentage = 100;
                                        tmpTable.SpacingBefore = 3.0F;
                                        //myDocument.Add(paragraphHeader)

                                        if (HeaderNewline == true)
                                        {
                                            for (int ttcolCount = 0; ttcolCount < arrpara.Length; ttcolCount++)
                                            {
                                                Phrase fontsizeHeader = new Phrase(arrHeaderName[ttcolCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                                                tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[ttcolCount];
                                                tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                                                if (hdBorder == false)
                                                {
                                                    //tmpTable.DefaultCell.Border = Rectangle.NO_BORDER + (BorderStyle)border[ttcolCount]; //to be checked 
                                                }
                                                else
                                                {
                                                    //tmpTable.DefaultCell.Border = ((BorderStyle)border(ttcolCount)) + Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked- done
                                                }

                                                tmpTable.AddCell(fontsizeHeader);
                                            }
                                        }

                                    }
                                }
                                tmpTable.DefaultCell.Colspan = arrpara.Length;
                                tmpTable.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                tmpTable.DefaultCell.Border = Rectangle.NO_BORDER;
                            }
                            if (!tMiddle)
                            {
                                tmpTable.AddCell(phCell);
                            }
                            else
                            {
                                if (colCount == 0)
                                {
                                    tmpTable.AddCell(phCell);
                                }
                            }

                            //If str <> "_SrNo" Then
                            colCount += 1;
                            //End If

                        }
                        if (tMiddle)
                        {
                            tmpTable.DefaultCell.Colspan = 1;
                        }



                        tSrNo = tSrNo + 1;
                    }
                }
                //Next;
            }
            else
            {
            }
            myDocument.Add(tmpTable);

            Table FooterTable = new Table(4);
            Cell FooterCell = new Cell();

            int[] intFooterWidth = new int[4];
            intFooterWidth[0] = 10;
            intFooterWidth[1] = 40;
            intFooterWidth[2] = 10;
            intFooterWidth[3] = 40;

            FooterTable.SetWidths(intFooterWidth);
            FooterTable.WidthPercentage = 100;
            FooterTable.Border = Rectangle.NO_BORDER;
            FooterTable.Cellpadding = 1f;

            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 15f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Colspan = 4;
            FooterCell.Rowspan = 3;
            FooterCell.Border = Rectangle.TOP_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_LEFT;
            FooterCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            FooterTable.AddCell(FooterCell);

            ChunkTitle = new Chunk("I/We hereby confirm the above", FontFactory.GetFont("c:\\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 7.5f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Colspan = 2;
            FooterCell.Border = Rectangle.NO_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_LEFT;
            FooterCell.VerticalAlignment = Element.ALIGN_TOP;
            FooterTable.AddCell(FooterCell);

            ChunkTitle = new Chunk("Yours Faithfully,", FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 7.5f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Colspan = 2;
            FooterCell.Border = Rectangle.NO_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            FooterCell.VerticalAlignment = Element.ALIGN_TOP;
            FooterTable.AddCell(FooterCell);

            //I.T. PAN NO. :
            ChunkTitle = new Chunk("", FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 50f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Border = Rectangle.NO_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_LEFT;
            FooterCell.VerticalAlignment = Element.ALIGN_TOP;
            FooterTable.AddCell(FooterCell);

            //& ToDt.Rows(0).Item("PanNo").ToString
            ChunkTitle = new Chunk(" ", FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, 8, Font.NORMAL));
            NPhrase = new Phrase(ChunkTitle);
            NPhrase.Leading = 50f;

            FooterCell = new Cell(NPhrase);
            FooterCell.Border = Rectangle.NO_BORDER;
            FooterCell.HorizontalAlignment = Element.ALIGN_LEFT;
            FooterCell.VerticalAlignment = Element.ALIGN_TOP;
            FooterTable.AddCell(FooterCell);

            myDocument.Add(FooterTable);


            if (strReportFooter != "")
            {
                Phrase PhReportFooter = new Phrase(strReportFooter, FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                myDocument.Add(PhReportFooter);
            }
            return mStream;
        }
        catch (DocumentException de)
        {
            throw de;


        }
        catch (IOException ioe)
        {
            throw ioe;
        }
        catch (Exception EX)
        {
            throw EX;

        }
        finally
        {
            myDocument.Close();
        }

    }

    public static MemoryStream CreatePDF_Index(string pdfName, string para, string strWidth, string strAlign, string ReportHeader, DataTable dt, string strHeaderName, bool bbBorder = true, bool hdBorder = true, string CompanyName = "", bool DateRequired = true, bool HtmlParse = false, bool isCmdLine = false, string strAddress = "", int VerticalAlignment = 5, bool OrntLandscap = false, bool isPrintAddress = false, int FontSize = 8, bool LeftRightStyle = false, int LeftPortionColCount = 1, int ReportHeaderAlign = Element.ALIGN_CENTER, string strReportFooter = "", string StrLocation = "L", bool HeaderNewline = true, int LineHeight = 0, bool LRBorder = false)
    {
        bool Firstcall = true, Secondcall = true;
        int IndexpageN = 0;
        List<Tuple<int, string, int>> IndexData = new List<Tuple<int, string, int>>();
        List<PdfPTable> tmpTables = new List<PdfPTable>();
    Exitlabel:
        Document myDocument = null;
        System.IO.MemoryStream mStream = null;
        int intSpace = 0;
        if (OrntLandscap == true)
        {
            myDocument = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);
            //edited by niyati to put date at right side
            intSpace = 460;
        }
        else
        {
            myDocument = new Document(PageSize.A4, 25, 25, 25, 25);
            //intSpace = 250
            //edited by niyati to put date at right side
            intSpace = 310;
        }



        string[] arrpara = null;
        string[] Indexarrpara = null;
        string[] arrWidth = null;
        string[] arrAlign = null;
        string[] arrHeaderName = null;
        HeaderFooter header1 = null;
        pdfName = pdfName + ".pdf";

        try
        {
            Phrase phCompanyName = new Phrase((string.IsNullOrEmpty(CompanyName) ? "" : CompanyName + "\n"), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize + 2, Font.BOLD));
            Phrase phCompanyName1 = new Phrase(("\n" + ReportHeader), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));


            Phrase phNewLine = new Phrase(Environment.NewLine + "");
            Phrase fontsize1 = new Phrase("", new Font(Font.GetFamilyIndex("Microsoft Sans Serif"), FontSize, Font.BOLD));

            PdfWriter writer = null;

            mStream = new System.IO.MemoryStream();
            writer = PdfWriter.GetInstance(myDocument, mStream);
            writer.CloseStream = false;

            //writer = PdfWriter.GetInstance(myDocument, new FileStream("D:\\temp\\" + pdfName, FileMode.Create));

            // create add the event handler 
            MyPageEvents events = new MyPageEvents();
            writer.PageEvent = events;

            header1 = new HeaderFooter(new Phrase(7.0F, ""), false);
            header1.Alignment = Element.ALIGN_CENTER;
            header1.BorderWidth = 0.5F;
            header1.Border = Rectangle.NO_BORDER;

            header1.Before.Add(0, phCompanyName);
            header1.Before.Add(1, phCompanyName1);

            if (isPrintAddress)
            {
                header1.Before.Add(1, new Phrase((string.IsNullOrEmpty(strAddress) ? " " + Environment.NewLine : Environment.NewLine + strAddress).ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize)));
            }

            header1.Alignment = Element.ALIGN_CENTER;

            myDocument.Header = header1;
            header1 = new HeaderFooter(new Phrase(7.0F, Environment.NewLine, FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD)), false);

            Font fnt = new Font(Font.GetFamilyIndex("Microsoft Sans Serif"), 6, Font.BOLD);
            if (DateRequired == true)
            {
                //header1.Before.Add(0, New Phrase((Format(Date.Now(), "dd/MMM/yyyy")).ToString + constPoweredBy.PadLeft(intSpace, " "), fnt))
                //edited by niyati to remove powered by

                header1.Before.Add(0, new Phrase((DateTime.Now.ToString("dd/MMM/yyyy").PadLeft(intSpace, ' ')).ToString(), fnt));
                //''''''''''''
            }
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                if (!Common.ConvertDBnullToBool(HttpContext.Current.Session["DoNotPrintPDFFooter"]))
                {
                    header1.Before.Add(0, new Phrase("Powered by www.ziperp.net", fnt));
                    header1.Alignment = Element.ALIGN_LEFT;
                }
            }
            else
            {
                string code = Common.GetIdByNameGeneralisedMethod("EnterpriseId", "Name", "Tenants", CompanyName, "", "");
                if (Common.CheckExistance("1", "DoNotPrintPDFFooter", "Enterprises", "and code = '" + code + "'", true, false, "") <= 0)
                {
                    header1.Before.Add(0, new Phrase("Powered by www.ziperp.net", fnt));
                    header1.Alignment = Element.ALIGN_LEFT;
                }
            }
            header1.Border = Rectangle.NO_BORDER;
            header1.BorderWidthTop = 0.5F;
            myDocument.Footer = header1;

            myDocument.Open();

            Paragraph paragraphHeader = new Paragraph(ReportHeader, FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
            string strHd;
            strHd = CompanyName + " - " + ReportHeader;
            paragraphHeader.Alignment = ReportHeaderAlign;
            //myDocument.Add(paragraphHeader);

            arrpara = para.Split('|');
            arrWidth = strWidth.Split('|');
            arrAlign = strAlign.Split('|');
            arrHeaderName = strHeaderName.Split('|');
            int colCount = dt.Columns.Count;
            int rowCount = dt.Rows.Count;
            Indexarrpara = new string[] { "S. No", "Particulars", "Page No" };
            PdfPTable IndexTable = new PdfPTable(Indexarrpara.Length);

            PdfPTable tmpTable = new PdfPTable(arrHeaderName.Length);
            PdfPTable originalTable = new PdfPTable(arrHeaderName.Length);

            int[] headerWidth = new int[arrWidth.Length];

            //INSTANT C# NOTE: Commented this declaration since looping variables in 'foreach' loops are declared in the 'foreach' header in C#:
            //			string width = null;
            int j = 0;
            foreach (string width in arrWidth)
            {
                headerWidth[j] = Convert.ToInt32(width);
                j = j + 1;
            }

            int[] bodyAlign = new int[arrAlign.Length];
            int[] border = new int[arrAlign.Length];
            int[] IndexbodyAlign = new int[] { 1, 0, 1 };
            int[] IndexheaderWidth = new int[] { 15, 65, 15 };

            setAlignBorder(arrAlign, ref bodyAlign, ref border); //to be checked 



            IndexTable.SetWidths(IndexheaderWidth);
            IndexTable.WidthPercentage = 100;
            IndexTable.SpacingBefore = 3.0F;

            tmpTable.SetWidths(headerWidth);
            tmpTable.WidthPercentage = 100;
            tmpTable.SpacingBefore = 3.0F;

            bool FlagHeader = true;
            if (FlagHeader == true)
            {

                for (colCount = 0; colCount < arrpara.Length; colCount++)
                {
                    Phrase fontsizeHeader = new Phrase(arrHeaderName[colCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                    //tmpTable.DefaultCell.GrayFill = 0.9F
                    if (LineHeight != 0 && !string.IsNullOrEmpty(Convert.ToString(LineHeight)))
                    {
                        tmpTable.DefaultCell.FixedHeight = LineHeight;
                    }

                    tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[colCount];
                    tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                    if (hdBorder == false)
                    {
                        tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                    }
                    else
                    {
                        if (LRBorder == true)
                        {
                            tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER; //to be checked  
                        }
                        else
                        {
                            tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked  
                        }
                    }

                    tmpTable.AddCell(fontsizeHeader);
                }

                for (colCount = 0; colCount < Indexarrpara.Length; colCount++)
                {
                    Phrase IndexfontsizeHeader = new Phrase((colCount == 1 ? "Index" : ""), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, 12, Font.BOLD));


                    IndexTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    IndexTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                    if (hdBorder == false)
                    {
                        IndexTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                    }
                    else
                    {
                        if (LRBorder == true)
                        {
                            IndexTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER; //to be checked  
                        }
                        else
                        {
                            IndexTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked  
                        }
                    }

                    IndexTable.AddCell(IndexfontsizeHeader);
                }
                for (colCount = 0; colCount < Indexarrpara.Length; colCount++)
                {
                    Phrase fontsizeHeader = new Phrase(Indexarrpara[colCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                    //IndexTable.DefaultCell.GrayFill = 0.9F
                    if (LineHeight != 0 && !string.IsNullOrEmpty(Convert.ToString(LineHeight)))
                    {
                        IndexTable.DefaultCell.FixedHeight = LineHeight;
                    }

                    IndexTable.DefaultCell.HorizontalAlignment = IndexbodyAlign[colCount];
                    IndexTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                    if (hdBorder == false)
                    {
                        IndexTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                    }
                    else
                    {
                        if (LRBorder == true)
                        {
                            IndexTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER; //to be checked  
                        }
                        else
                        {
                            IndexTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER; //to be checked  
                        }
                    }

                    IndexTable.AddCell(fontsizeHeader);
                }
                FlagHeader = false;
            }


            tmpTable.HeaderRows = 1;

            //tmpTable.DefaultCell.GrayFill = 1.0F
            tmpTable.DefaultCell.VerticalAlignment = 5;
            string str = "";
            bool isBold = false;
            bool isItalic = false;
            Font fnt11 = new Font();
            Font fnt12 = new Font();
            string[] LinePara = null;
            bool PrintLine = false;
            bool tMiddle = false;
            bool tNewPage = false;
            if (HtmlParse == false && Firstcall)
            {
                long tSrNo = 0;
                tSrNo = 1;
                foreach (DataRow row in dt.Rows)
                {
                    PrintLine = true;
                    colCount = 0;
                    isBold = false;
                    isItalic = false;
                    tMiddle = false;
                    tNewPage = false;

                    if (LeftRightStyle)
                    {
                        if (!(dt.Columns.Contains("_LeftStyle")))
                        {
                            fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {

                            if (string.IsNullOrEmpty(("" + row["_LeftStyle"])))
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_LeftStyle"]) == "B")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_LeftStyle"]) == "I")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_LeftStyle"]) == "BI")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                        }
                        if (!(dt.Columns.Contains("_RightStyle")))
                        {
                            fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(("" + row["_RightStyle"])))
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_RightStyle"]) == "B")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_RightStyle"]) == "I")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_RightStyle"]) == "BI")
                            {
                                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                        }

                    }
                    else
                    {

                        if (!(dt.Columns.Contains("_Style")))
                        {
                            fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(("" + row["_Style"])))
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);
                            }
                            if (("" + row["_Style"]) == "B")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD);
                            }
                            if (("" + row["_Style"]) == "I")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.ITALIC);
                            }
                            if (("" + row["_Style"]) == "BI")
                            {
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLDITALIC);
                            }
                            if (("" + row["_Style"]) == "H")
                            {
                                PrintLine = false;
                            }
                            if (("" + row["_Style"]) == "M")
                            {
                                tMiddle = true;
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.BOLD);
                            }
                            if (("" + row["_Style"]) == "MN")
                            {
                                tMiddle = true;
                                tNewPage = true;
                                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.BOLD + Font.UNDERLINE);
                            }
                            if(!tMiddle && dt.Rows.Count == tSrNo)
                            {
                                tNewPage = true;
                                tMiddle = true;
                            }
                        }
                    }


                    if (!(dt.Columns.Contains("cmdLine")))
                    {
                        LinePara = row["cmdLine"].ToString().Split('|');
                    }
                    else
                    {

                        LinePara = row["cmdLine"].ToString().Split('|');

                    }
                    if (PrintLine)
                    {

                        if (LinePara[LinePara.Length - 1] == "AB" && ("" + row["AccId"]) == "")
                        {
                            IndexData.Add(new Tuple<int, string, int>(IndexData.Count + 1, row["Particulars"].ToString(), writer.PageNumber));
                        }

                        foreach (var str1 in arrpara)
                        {

                            //Dim helvetica As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, True)
                            //Dim fnt11 As New Font(helvetica, 9, Font.NORMAL)
                            Phrase phCell = null;
                            if (str1 == "_SrNo")
                            {
                                phCell = new Phrase(tSrNo.ToString(), fnt11);
                            }
                            else
                            {
                                if (LeftRightStyle)
                                {
                                    if (colCount <= LeftPortionColCount)
                                    {
                                        phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt11);
                                    }
                                    else
                                    {
                                        phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt12);
                                    }

                                }
                                else
                                {
                                    phCell = new Phrase(row[arrpara[colCount].ToString()].ToString(), fnt11);
                                }

                            }

                            //Dim phCell As New Phrase(row(arrpara(colCount).ToString()).ToString, fnt11)

                            //tmpTable.DefaultCell.FixedHeight = 15
                            tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[colCount]; //to be checked 
                            tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                            //tmpTable.DefaultCell.VerticalAlignment = VerticalAlignment;
                            if (bbBorder == false)
                            {

                                tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked 
                            }
                            else
                            {
                                if (isCmdLine == true && LRBorder == true)
                                {
                                    if (LinePara[0] != "NLR")
                                    {
                                        tmpTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER; //to be checked  
                                    }
                                    else
                                    {
                                        tmpTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                                    }
                                }
                                else
                                {
                                    tmpTable.DefaultCell.Border = Rectangle.NO_BORDER;  //to be checked  
                                }
                            }


                            if (isCmdLine == true)
                            {
                                if (colCount <= LinePara.Length - 1)
                                {
                                    if (LinePara[colCount] == "A")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.TOP_BORDER;
                                    }
                                    else if (LinePara[colCount] == "B")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER;
                                    }
                                    else if (LinePara[colCount] == "AB")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER;
                                    }
                                }
                                else
                                {
                                    if (LinePara[LinePara.Length - 1] == "A")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.TOP_BORDER;
                                    }
                                    else if (LinePara[LinePara.Length - 1] == "B")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER;
                                    }
                                    else if (LinePara[LinePara.Length - 1] == "AB")
                                    {
                                        tmpTable.DefaultCell.Border += Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER;
                                    }
                                }

                            }
                            if (tMiddle && colCount == 0)
                            {
                                if (tNewPage == true)
                                {
                                    if (tSrNo != 1)
                                    {
                                        tmpTables.Add(tmpTable);
                                        myDocument.Add(tmpTable);
                                        myDocument.NewPage();
                                        tmpTable = new PdfPTable(arrHeaderName.Length);
                                        tmpTable.SetWidths(headerWidth);
                                        tmpTable.WidthPercentage = 100;
                                        tmpTable.SpacingBefore = 3.0F;
                                        //  myDocument.Add(paragraphHeader);

                                        if (HeaderNewline == true)
                                        {
                                            for (int ttcolCount = 0; ttcolCount < arrpara.Length; ttcolCount++)
                                            {
                                                Phrase fontsizeHeader = new Phrase(arrHeaderName[ttcolCount].ToString(), FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                                                tmpTable.DefaultCell.HorizontalAlignment = bodyAlign[ttcolCount];
                                                tmpTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                                                if (hdBorder == false)
                                                {
                                                    //tmpTable.DefaultCell.Border = Rectangle.NO_BORDER + (BorderStyle)border[ttcolCount]; //to be checked 
                                                }
                                                else
                                                {
                                                    tmpTable.DefaultCell.Border = Rectangle.TOP_BORDER;
                                                }

                                                tmpTable.AddCell(fontsizeHeader);
                                            }
                                        }

                                    }
                                }
                                tmpTable.DefaultCell.Colspan = arrpara.Length;
                                tmpTable.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                tmpTable.DefaultCell.Border = Rectangle.NO_BORDER;
                            }
                            if (!tMiddle)
                            {
                                tmpTable.AddCell(phCell);
                            }
                            else
                            {
                                if (colCount == 0)
                                {
                                    tmpTable.AddCell(phCell);
                                }
                            }

                            //If str <> "_SrNo" Then
                            colCount += 1;
                            //End If

                        }
                        if (tMiddle)
                        {
                            tmpTable.DefaultCell.Colspan = 1;
                        }



                        tSrNo = tSrNo + 1;
                    }
                }
                //Next;

            }
            else
            {

            }
            if (Firstcall)
            {
                tmpTables.Add(tmpTable);
                Firstcall = false;
                goto Exitlabel;
            }
            #region Index
            foreach (Tuple<int, string, int> IData in IndexData)
            {

                string[] row = new string[] { IData.Item1.ToString(), IData.Item2.ToString(), (IData.Item3 + IndexpageN).ToString() };
                colCount = 0;
                tMiddle = false;

                fnt12 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);

                fnt11 = FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, false, FontSize, Font.NORMAL);


                for (int i = 0; i < Indexarrpara.Count(); i++)
                {

                    //Dim helvetica As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arialuni.ttf", BaseFont.IDENTITY_H, True)
                    //Dim fnt11 As New Font(helvetica, 9, Font.NORMAL)
                    Phrase phCell = null;

                    if (LeftRightStyle)
                    {
                        if (colCount <= LeftPortionColCount)
                        {
                            phCell = new Phrase(row[i].ToString(), fnt11);
                        }
                        else
                        {
                            phCell = new Phrase(row[i].ToString(), fnt12);
                        }

                    }
                    else
                    {
                        phCell = new Phrase(row[i].ToString(), fnt11);
                    }


                    //Dim phCell As New Phrase(row(arrpara(colCount).ToString()).ToString, fnt11)

                    //IndexTable.DefaultCell.FixedHeight = 15
                    IndexTable.DefaultCell.HorizontalAlignment = IndexbodyAlign[colCount]; //to be checked 
                    IndexTable.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;//.ALIGN_MIDDLE;
                                                                                 //IndexTable.DefaultCell.VerticalAlignment = VerticalAlignment;
                    if (bbBorder == false)
                    {

                        IndexTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked 
                    }
                    else
                    {
                        if (isCmdLine == true && LRBorder == true)
                        {
                            if (LinePara[0] != "NLR")
                            {
                                IndexTable.DefaultCell.Border = Rectangle.BOTTOM_BORDER + Rectangle.TOP_BORDER + Rectangle.LEFT_BORDER + Rectangle.RIGHT_BORDER; //to be checked  
                            }
                            else
                            {
                                IndexTable.DefaultCell.Border = Rectangle.NO_BORDER; //to be checked  
                            }
                        }
                        else
                        {
                            IndexTable.DefaultCell.Border = Rectangle.NO_BORDER;  //to be checked  
                        }
                    }



                    if (tMiddle && colCount == 0)
                    {

                        IndexTable.DefaultCell.Colspan = arrpara.Length;
                        IndexTable.DefaultCell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        IndexTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    if (!tMiddle)
                    {
                        IndexTable.AddCell(phCell);
                    }
                    else
                    {
                        if (colCount == 0)
                        {
                            IndexTable.AddCell(phCell);
                        }
                    }

                    //If str <> "_SrNo" Then
                    colCount += 1;
                    //End If

                }
                if (tMiddle)
                {
                    IndexTable.DefaultCell.Colspan = 1;
                }
            }
            #endregion
            myDocument.Add(IndexTable);
            if (Secondcall)
            {
                IndexpageN = writer.PageNumber;
                Secondcall = false;
                goto Exitlabel;
            }
            if (tmpTables.Count > 0)
            {
                for (int i = 0; i < tmpTables.Count - 1; i++)
                {
                    int pageN = writer.PageNumber;
                    myDocument.NewPage();
                    myDocument.Add(tmpTables[i]);
                }
            }
            if (strReportFooter != "")
            {
                Phrase PhReportFooter = new Phrase(strReportFooter, FontFactory.GetFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, true, FontSize, Font.BOLD));
                myDocument.Add(PhReportFooter);
            }
            return mStream;
        }
        catch (DocumentException de)
        {
            throw de;


        }
        catch (IOException ioe)
        {
            throw ioe;
        }
        catch (Exception EX)
        {
            throw EX;

        }
        finally
        {
            myDocument.Close();
        }

    }

}



