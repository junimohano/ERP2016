using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using GemBox.Spreadsheet;

public class GemboxService
{
    // 라이센스 키
    private const string licenseKey = "EIKU-1Y5V-Y7R4-ASNB";

    public GemboxService()
    {
        setLicense();
    }

    /// <summary>
    ///     라이센스 등록하기
    /// </summary>
    private void setLicense()
    {
        SpreadsheetInfo.SetLicense(licenseKey);
    }

    public ExcelFile SetExcelFile(DataTable dt, string reportSubject)
    {
        var excelFile = new ExcelFile();
        try
        {
            var ewSheet = excelFile.Worksheets.Add(reportSubject);
            ewSheet.InsertDataTable(dt, 0, 0, true);
        }
        catch (Exception ex)
        {
            Debug.Print(ex.Message);
        }
        return excelFile;
    }

    //public ExcelFile SetExcelFile(DataTable dt, string reportSubject)
    //{
    //    var excelFile = new ExcelFile();
    //    try
    //    {
    //        var ewSheet = excelFile.Worksheets.Add(reportSubject);

    //        var columnCount = dt.Columns.Count;

    //        // 제목
    //        ewSheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount - 2).Merged = true;
    //        ewSheet.Cells[0, 0].Style.Font.Size = 18*20;
    //        ewSheet.Cells[0, 0].Style.Font.Weight = ExcelFont.BoldWeight;
    //        ewSheet.Cells[0, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
    //        ewSheet.Cells[0, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
    //        ewSheet.Cells[0, 0].SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
    //        ewSheet.Cells[0, 0].Value = reportSubject;

    //        var date = DateTime.Now;

    //        // 날짜
    //        ewSheet.Cells[0, columnCount - 1].Value = date.Year + "-" + string.Format("{0:d2}", date.Month) + "-" +
    //                                                  string.Format("{0:d2}", date.Day);
    //        ewSheet.Cells[0, columnCount - 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
    //        ewSheet.Cells[0, columnCount - 1].Style.Font.Size = 14*20;
    //        ewSheet.Cells[0, columnCount - 1].Style.Font.Weight = ExcelFont.BoldWeight;
    //        ewSheet.Cells[0, columnCount - 1].SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);

    //        //---------------------------------------------------------------
    //        // 헤더 설정
    //        //---------------------------------------------------------------
    //        for (var i = 0; i < columnCount; i++)
    //        {
    //            //---------------------------------------------------------------------------------
    //            // 이름.
    //            //---------------------------------------------------------------------------------
    //            ewSheet.Cells[1, i].Value = dt.Columns[i].ToString();
    //            //---------------------------------------------------------------------------------
    //            // 색깔.
    //            //---------------------------------------------------------------------------------
    //            ewSheet.Cells[1, i].Style.FillPattern.SetSolid(Color.CornflowerBlue);
    //            //---------------------------------------------------------------------------------
    //            // 글씨.
    //            //---------------------------------------------------------------------------------
    //            ewSheet.Cells[1, i].Style.Font.Color = Color.White;
    //            ewSheet.Cells[1, i].Style.Font.Weight = ExcelFont.BoldWeight;
    //            //---------------------------------------------------------------------------------
    //            // 줄.
    //            //---------------------------------------------------------------------------------
    //            ewSheet.Cells[1, i].SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);

    //            //---------------------------------------------------------------------------------
    //            // 세로 정렬
    //            //---------------------------------------------------------------------------------
    //            ewSheet.Cells[1, i].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
    //            //---------------------------------------------------------------------------------
    //            // 가로 정렬
    //            //---------------------------------------------------------------------------------
    //            ewSheet.Cells[1, i].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
    //        }

    //        //---------------------------------------------------------------
    //        // 리포트 행 데이터 입력.
    //        //---------------------------------------------------------------
    //        var blankRow = 2;

    //        for (var i = 0; i < dt.Rows.Count; i++)
    //        {
    //            for (var j = 0; j < columnCount; j++)
    //            {
    //                // 값 입력
    //                ewSheet.Cells[i + blankRow, j].Value = dt.Rows[i][j].ToString();

    //                //---------------------------------------------------------------------------------
    //                // 줄.
    //                //---------------------------------------------------------------------------------
    //                ewSheet.Cells[i + blankRow, j].SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
    //                //---------------------------------------------------------------------------------
    //                // 세로 정렬
    //                //---------------------------------------------------------------------------------
    //                ewSheet.Cells[i + blankRow, j].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
    //                //---------------------------------------------------------------------------------
    //                // 가로 정렬
    //                //---------------------------------------------------------------------------------
    //                ewSheet.Cells[i + blankRow, j].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
    //            }
    //        }

    //        //---------------------------------------------------------------------------------
    //        // 핏
    //        //---------------------------------------------------------------------------------
    //        for (var i = 0; i < columnCount; i++)
    //        {
    //            ewSheet.Columns[i].AutoFit();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.Print(ex.Message);
    //    }
    //    return excelFile;
    //}


    /// <summary>
    ///     엑셀파일 여러개 로드하기
    /// </summary>
    /// <param name="excelFileList">생성한 엑셀파일 리스트</param>
    /// <param name="fileNames">파일 경로</param>
    /// <returns></returns>
    public bool isLoadFile(List<ExcelFile> excelFileList, string[] filePath)
    {
        try
        {
            // 초기화
            excelFileList.Clear();

            // 엑셀 파일 만큼 돌면서
            for (var i = 0; i < filePath.Length; i++)
            {
                excelFileList.Add(new ExcelFile());
                //확장자에 맞는 엑셀 파일 열기
                if (new FileInfo(filePath[i]).Extension == ".xls")
                    excelFileList[i].LoadXls(filePath[i]);
                else
                    excelFileList[i].LoadXlsx(filePath[i], XlsxOptions.None);
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.Print(ex.Message);
            return false;
        }
    }


    /// <summary>
    ///     엑셀파일 하나 로드하기
    /// </summary>
    /// <param name="excelFile">생성한 엑셀파일</param>
    /// <param name="filePath">파일 경로</param>
    /// <returns></returns>
    public bool isLoadFile(ExcelFile excelFile, string filePath)
    {
        try
        {
            //확장자에 맞는 엑셀 파일 열기
            if (new FileInfo(filePath).Extension == ".xls")
                excelFile.LoadXls(filePath);
            else
                excelFile.LoadXlsx(filePath, XlsxOptions.None);
            return true;
        }
        catch (Exception ex)
        {
            Debug.Print(ex.Message);
            return false;
        }
    }


    /// <summary>
    ///     엑셀 셀 데이터 가져오기
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public string getCellValue(ExcelCell data)
    {
        // 값이 없으면
        if (data.Value == null)
            // 공백
            return string.Empty;
        // 값이 있으면
        return data.Value.ToString();
    }

    /// <summary>
    ///     엑셀 셀 데이터 소문자로 가져오기
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public string getCellValueToLower(ExcelCell data)
    {
        // 소문자 변환
        return getCellValue(data).ToLower();
    }

    /// <summary>
    ///     검색스트링으로 엑셀파일 A(첫번째컬럼)의 Row찾기
    /// </summary>
    /// <param name="excel"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public int getFindRow(ExcelWorksheet excel, string name)
    {
        // 행 수만큼
        for (var i = 0; i < excel.Rows.Count; i++)
        {
            // 원하는 스트링 찾아서
            if (getCellValueToLower(excel.Rows[i].Cells[0]) == name.ToLower())
                return i;
        }
        return -1;
    }

    /// <summary>
    ///     row열에 해당하는 마지막 Interface(컬럼) 인덱스 가져오기
    /// </summary>
    /// <param name="excel"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    public int getFindColumnLastIndex(ExcelWorksheet excel, int row)
    {
        if (row == -1)
            return -1;

        // 컬럼 수만큼
        //for (int i = 1; i < excel.Cells.LastColumnIndex; i++)
        for (var i = 1; i < excel.CalculateMaxUsedColumns() + 1; i++)
        {
            // 비어있는 셀 찾아서
            if (getCellValue(excel.Rows[row].Cells[i]) == string.Empty)
                return i - 1;
        }
        return -1;
    }

    /// <summary>
    ///     검색스트링으로 row에 컬럼들중에서 스트링과 같은 문자열인 컬럼 인덱스 가져오기
    /// </summary>
    /// <param name="excel"></param>
    /// <param name="row"></param>
    /// <param name="data"></param>
    /// <param name="columnCount"></param>
    /// <returns></returns>
    public int getFindColumnLastIndexFromColumnName(ExcelWorksheet excel, int row, string data, int columnCount)
    {
        // 컬럼 수만큼
        for (var i = 1; i <= columnCount; i++)
        {
            // 비어있는 셀 찾아서
            if (getCellValueToLower(excel.Rows[row].Cells[i]) == data.ToLower())
                return i;
        }
        return -1;
    }

    /// <summary>
    ///     검색스트링으로 row에 컬럼들중에서 스트링에 포함하는 컬럼 인덱스 가져오기
    /// </summary>
    /// <param name="excel"></param>
    /// <param name="row"></param>
    /// <param name="data">검색할 스트링</param>
    /// <param name="columnCount"></param>
    /// <param name="isStandardSearch">시작부터 돌면 = true, 끝에서 부터 돌면 = false</param>
    /// <returns></returns>
    public int getFindColumnIndexFormContainColumnName(ExcelWorksheet excel, int row, string data, int columnCount,
        bool isStandardSearch)
    {
        // true이면 시작점 부터
        if (isStandardSearch)
        {
            // 컬럼 수만큼
            for (var j = 1; j <= columnCount; j++)
            {
                // 비어있는 셀 찾아서
                if (getCellValueToLower(excel.Rows[row].Cells[j]).StartsWith(data.ToLower()))
                    return j;
            }
        }
        //false 이면 거꾸로
        else
        {
            // 컬럼 수만큼
            for (var j = columnCount; j > 0; j--)
            {
                // 비어있는 셀 찾아서
                if (getCellValueToLower(excel.Rows[row].Cells[j]).StartsWith(data.ToLower()))
                    return j;
            }
        }
        return -1;
    }

    public bool isCommentCheck(ExcelWorksheet excel, int row)
    {
        if (getCellValue(excel.Rows[row].Cells[0]) == "!")
            return true;
        return false;
    }

    /// <summary>
    ///     해당 Row가 빈행인지 확인
    /// </summary>
    /// <param name="excel"></param>
    /// <param name="row"></param>
    /// <param name="columnCount"></param>
    /// <returns></returns>
    public bool isBlankCheck(ExcelWorksheet excel, int row, int columnCount)
    {
        var blankCheck = true;

        for (var j = 0; j <= columnCount; j++)
        {
            if (getCellValue(excel.Rows[row].Cells[j]) != string.Empty)
                blankCheck = false;
        }
        return blankCheck;
    }

    /// <summary>
    ///     셀에 값넣기(가로, 세로 중앙 fit)
    /// </summary>
    /// <param name="excelCell"></param>
    /// <param name="value"></param>
    public void setCell(ExcelCell excelCell, string value)
    {
        excelCell.Value = value;
        excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        excelCell.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
    }

    /// <summary>
    ///     셀에 값 넣기(가로, 세로 중앙fit + 글자 진하게)
    /// </summary>
    /// <param name="excelCell"></param>
    /// <param name="value"></param>
    public void setCellBold(ExcelCell excelCell, string value)
    {
        setCell(excelCell, value);
        excelCell.Style.Font.Weight = ExcelFont.BoldWeight;
    }

    /// <summary>
    ///     셀에 값 넣기(가로, 세로 중앙fit + 셀 박스)
    /// </summary>
    /// <param name="excelCell"></param>
    /// <param name="value"></param>
    public void setCellBox(ExcelCell excelCell, string value)
    {
        setCell(excelCell, value);
        excelCell.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
    }

    /// <summary>
    ///     셀에 값 넣기(가로, 세로 중앙fit + 글자 진하게 + 셀 박스)
    /// </summary>
    /// <param name="excelCell"></param>
    /// <param name="value"></param>
    public void setCellBoldAndBox(ExcelCell excelCell, string value)
    {
        setCellBold(excelCell, value);
        excelCell.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
    }
}