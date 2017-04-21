using System;
using System.Collections.Generic;
using System.Linq;
using MigrationToTallyERP9.CSVParser;

namespace MigrationToTallyERP9
{
    public class BarcodeValueGen
    {
        private List<int> mBarcodesToIgnoreList = new List<int>();

        private int mLastUsedBarcode;

        private BarcodeValueGen()
        {
            mLastUsedBarcode = RetrieveLastUsedBarcodeFromFile();
        }

        private static BarcodeValueGen _instance;
        public static BarcodeValueGen Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BarcodeValueGen();    

                return _instance;
            }
        }
        public int GetMeNewBarcode()
        {
            mLastUsedBarcode = GetNextAvailableBarcode(mLastUsedBarcode);
            return mLastUsedBarcode;
        }

        private int RetrieveLastUsedBarcodeFromFile()
        {
            var barcodesCSVParams = ParseCSVFiles.ParseCSV<BarcodeCSVParams, BarcodeCSVParamsMap>(Configurations.BarcodeCSVFilePath);

            string lastUsedBarcodeStr = barcodesCSVParams.Last().LastBarcodeAssigned;
            int lastUsedBarcodeInt;

            if (!int.TryParse(lastUsedBarcodeStr, out lastUsedBarcodeInt))
                throw new Exception($"Barcode {lastUsedBarcodeStr} not an integer! Please check the barcodes CSV file");

            return lastUsedBarcodeInt;
        }

        private int GetNextAvailableBarcode(int lastUsedBarcode)
        {
            int nextBarcode = lastUsedBarcode + 1;

            if (mBarcodesToIgnoreList.Contains(nextBarcode))
                return GetNextAvailableBarcode(nextBarcode);
            else
                return nextBarcode;
        }
    }
}