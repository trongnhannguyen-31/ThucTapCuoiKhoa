using System;
using System.Web;
using Phoenix.Server.Services.Database;

namespace CongDongBau.Server.Services.Import
{
    public class ImportExcelManager
    {
        private readonly DataContext _dataContext;
        public ImportExcelManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region Utilities

        protected virtual int GetColumnIndex(string[] properties, string columnName)
        {
            if (properties == null)
                throw new ArgumentNullException("properties");

            if (columnName == null)
                throw new ArgumentNullException("columnName");

            for (int i = 0; i < properties.Length; i++)
                if (properties[i].Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return i + 1; //excel indexes start from 1
            return 0;
        }

        protected virtual string ConvertColumnToString(object columnValue)
        {
            if (columnValue == null)
                return null;

            return Convert.ToString(columnValue);
        }

        protected virtual string GetMimeTypeFromFilePath(string filePath)
        {
            var mimeType = MimeMapping.GetMimeMapping(filePath);

            //little hack here because MimeMapping does not contain all mappings (e.g. PNG)
            if (mimeType == "application/octet-stream")
                mimeType = "image/jpeg";

            return mimeType;
        }

        #endregion

        #region Methods

        #region Import Member Card
        /*
        public virtual List<ImportMemberCardDto> ImportMemberCard(Stream stream, bool isCheck = true)
        {
            var res = new List<ImportMemberCardDto>();
            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new FalconException("No worksheet found");

                //the columns
                var properties = new[]
                {
                    "STT",
                    "MÃ THẺ",
                    "MÃ XÁC NHẬN",
                    "LOẠI THẺ"
                };

                var iRow = 5;
                const int index = 4;
                while (true)
                {
                    var allColumnsAreEmpty = true;
                    for (var i = 1; i <= properties.Length; i++)
                        if (worksheet.Cells[iRow, i].Value != null && !string.IsNullOrEmpty(worksheet.Cells[iRow, i].Value.ToString()))
                        {
                            allColumnsAreEmpty = false;
                            break;
                        }
                    if (allColumnsAreEmpty)
                        break;
                    var card = new ImportMemberCardDto();
                    //Stt
                    string stt = ConvertColumnToString(worksheet.Cells[iRow, GetColumnIndex(properties, "STT")].Value);
                    card.Id = int.Parse(stt);
                    //card code
                    var code = ConvertColumnToString(worksheet.Cells[iRow, GetColumnIndex(properties, "MÃ THẺ")].Value);
                    code = !string.IsNullOrEmpty(code) ? code.Trim() : code;
                    if (string.IsNullOrEmpty(code))
                    {
                        card.DataValid += "Thiếu mã thẻ;";
                    }
                    card.Code = code;
                    var existCard = _dataContext.Cards.AsNoTracking().FirstOrDefault(d => d.Code == code);
                    if (existCard != null)
                    {
                        card.DataValid += "Mã thẻ đã tồn tại;";
                    }
                    //secret code
                    var secretCode = ConvertColumnToString(worksheet.Cells[iRow, GetColumnIndex(properties, "MÃ XÁC NHẬN")].Value);
                    if (string.IsNullOrEmpty(secretCode) || secretCode.Length > 6)
                    {
                        card.DataValid += "Mã xác nhận không hợp lệ;";
                    }
                    card.SecretCode = secretCode;
                    //card type
                    string cardTypeName = ConvertColumnToString(worksheet.Cells[iRow, GetColumnIndex(properties, "LOẠI THẺ")].Value);
                    if (string.IsNullOrEmpty(cardTypeName))
                    {
                        card.DataValid += "Chưa chọn loại thẻ;";
                    }
                    card.CardTypeName = cardTypeName;
                    var existCardType = _dataContext.Card_Types.AsNoTracking().FirstOrDefault(d => d.Name == cardTypeName);
                    if (existCardType == null)
                    {
                        card.DataValid += "Loại thẻ không đúng;";
                    }
                    if (string.IsNullOrEmpty(card.DataValid))
                        card.DataValid = "Ok";
                    res.Add(card);
                    if (!isCheck && card.IsValid)
                    {
                        //add to Db
                        var cardEntity = card.MapTo<Card>();
                        cardEntity.CardTypeId = existCardType.Id;
                        cardEntity.CardStatusId = _lookupService.GetLookupIdByCode(LookupEnums.CardStatus.Inactive.ToString());
                        _cardService.Insert(cardEntity);
                    }
                    //next row
                    iRow++;
                }
                return res;
            }
        }
        */
        #endregion

        #endregion
    }
}
