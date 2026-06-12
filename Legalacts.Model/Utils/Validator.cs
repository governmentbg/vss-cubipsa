using System.Linq;

namespace Legalacts.Model.Utils
{
    public class Validator
    {
        /// <summary>
        /// Chack if the given format is MIME valid formats
        /// </summary>
        /// <param name="textType"></param>
        /// <returns></returns>
        public static bool CheckTextType(string textType)
        {
            string[] set = { "text/plain", "text/html", "application/msword", "application/pdf" };
            return set.Contains(textType);
        }


        public static bool CheckActConnectType(int connectType)
        {
            int[] set = {   3001,
                            3002,
                            3003,
                            3004,
                            3005,
                            3006
                        };

            return set.Contains(connectType);
        }

        /// <summary>
        /// Check if the given Act Kind exists
        /// </summary>
        /// <param name="actKind"></param>
        /// <returns></returns>
        public static bool CheckActKind(int actKind)
        {
            int[] set = {   5001,
                            5002,
                            5003,
                            5004,
                            5005,
                            5006,
                            5007,
                            5008,
                            5009,
                            5010,
                            5011
                        };
            return set.Contains(actKind);
        }

        /// <summary>
        /// Check if the given Court exists
        /// </summary>
        /// <param name="court"></param>
        /// <returns></returns>
        public static bool CheckCourt(int court)
        {
            int[] set = {   001,
                    002,
                    100,
                    110,
                    111,
                    120,
                    121,
                    122,
                    123,
                    124,
                    125,
                    130,
                    131,
                    132,
                    133,
                    140,
                    141,
                    142,
                    143,
                    144,
                    145,
                    146,
                    150,
                    151,
                    152,
                    160,
                    161,
                    162,
                    163,
                    170,
                    171,
                    172,
                    173,
                    174,
                    180,
                    181,
                    182,
                    183,
                    184,
                    185,
                    186,
                    187,
                    188,
                    189,
                    200,
                    210,
                    211,
                    212,
                    213,
                    214,
                    215,
                    216,
                    217,
                    218,
                    220,
                    221,
                    222,
                    223,
                    230,
                    231,
                    232,
                    233,
                    300,
                    310,
                    311,
                    312,
                    313,
                    320,
                    321,
                    322,
                    323,
                    324,
                    325,
                    330,
                    331,
                    332,
                    333,
                    340,
                    341,
                    342,
                    343,
                    350,
                    351,
                    352,
                    353,
                    360,
                    361,
                    362,
                    363,
                    400,
                    410,
                    411,
                    412,
                    413,
                    414,
                    415,
                    420,
                    421,
                    422,
                    423,
                    424,
                    430,
                    431,
                    432,
                    433,
                    434,
                    440,
                    441,
                    442,
                    443,
                    444,
                    450,
                    451,
                    452,
                    500,
                    510,
                    511,
                    513,
                    514,
                    515,
                    520,
                    521,
                    522,
                    523,
                    524,
                    530,
                    531,
                    532,
                    533,
                    534,
                    540,
                    541,
                    542,
                    543,
                    544,
                    545,
                    550,
                    551,
                    552,
                    553,
                    554,
                    555,
                    560,
                    561,
                    562,
                    563,
                    564,
                    565,
                    600,
                    610,
                    620,
                    630,
                    640,
                    650,
                    701,
                    702,
                    703,
                    704,
                    705,
                    706,
                    707,
                    708,
                    709,
                    710,
                    711,
                    712,
                    713,
                    714,
                    715,
                    716,
                    717,
                    718,
                    719,
                    720,
                    721,
                    722,
                    723,
                    724,
                    725,
                    726,
                    727,
                    728,
                    101,
                    105,
                        };
            return set.Contains(court);
        }

        /// <summary>
        /// Check if the given Case Kind exists
        /// </summary>
        /// <param name="caseKind"></param>
        /// <returns></returns>
        public static bool CheckCaseKind(int caseKind)
        {
            int[] set = {   2001,
                            2002,
                            2003,
                            2004,
                            2005,
                            2006,
                            2007,
                            2008,
                            2009,
                            2010,
                            2011,
                            2012,
                            2013,
                            2014,
                            2015,
                            2016,
                            2017,
                            2018,
                            2019,
                            2020,
                            2021,
                            2022,
                            2023,
                            2024,
                            2025,
                            2026,
                            2027,
                            2028,
                            2029,
                            2030,
                            2031,
                            2032,
                            2033,
                            2034,
                        };
            return set.Contains(caseKind);
        }

        /// <summary>
        /// Check if the given Kind Of Appeal Act exists
        /// </summary>
        /// <param name="typeOfAppealAct"></param>
        /// <returns></returns>
        public static bool CheckTypeOfAppealAct(int typeOfAppealAct)
        {
            int[] set = {   6001,
                            6002,
                            6003,
                            6004,
                            6005,
                            6006,
                            6007,
                            6008,
                            6009,
                            6010,
                            6011,
                            6012,
                            6013,
                            6014,
                            6015,
                            6016,
                        };
            return set.Contains(typeOfAppealAct);
        }

        /// <summary>
        /// Check if the given Connected Type exists
        /// </summary>
        /// <param name="connectedType"></param>
        /// <returns></returns>
        public static bool CheckConnectedType(int connectedType)
        {
            int[] set = {   3001,
                            3002,
                            3003,
                            3004,
                            3005
                        };
            return set.Contains(connectedType);
        }

        /// <summary>
        /// Check if the given Connected Kind exists
        /// </summary>
        /// <param name="connectedKind"></param>
        /// <returns></returns>
        public static bool CheckConnectedKind(int connectedKind)
        {
            int[] set = {   4001,
                            4002,
                            4003,
                            4004,
                            4005,
                            4006,
                            4007,
                            4008,
                            4009
                        };
            return set.Contains(connectedKind);
        }

         /// <summary>
        /// Check if the given Sendto Document exists
        /// </summary>
        /// <param name="sendtoDoc"></param>
        /// <returns></returns>
        public static bool CheckSendToDoc(int sendtoDoc)
        {
            int[] set = {   7001,
                            7002,
                            7003,
                            7004,
                            7005
                        };
            return set.Contains(sendtoDoc);
        }

        public static bool CheckResultOfAppealCivilCase(string resultOfAppeal)
        {
            string[] set = {
                            "1",
                            "2",
                            "3",
                            "3а",
                            "3б",
                            "3в",
                            "3г",
                            "4",
                            "5",
                            "5а",
                            "5б",
                            "5в",
                            "5г",
                            "6",
                            "6а",
                            "6б",
                            "6в",
                            "6г",
                            "7",
                            "7а",
                            "7б",
                            "7в",
                            "7г"
                           };

            return set.Contains(resultOfAppeal);
        }

        public static bool CheckResultOfAppealCivilCaseAdministrativeCourt(string resultOfAppeal)
        {
            string[] set = {
                            "1",
                            "2",
                            "2а",
                            "2б",
                            "3",
                            "4",
                            "4а-1",
                            "4а-2",
                            "4а-3",
                            "5"
                           };

            return set.Contains(resultOfAppeal);
        }

        public static bool CheckResultOfAppealCriminalCase(string resultOfAppeal)
        {
            string[] set = {
                            "1",
                            "2",
                            "2а",
                            "2б",
                            "2в",
                            "2г",
                            "3",
                            "3а",
                            "3б",
                           };

            return set.Contains(resultOfAppeal);
        }

        public static bool CheckActNumber(int actno)
        {
            return (actno > 0 && actno.ToString().Length <= 6);
        }

        public static bool CheckCaseNumber(int caseno)
        {
            return (caseno > 0 && caseno.ToString().Length <= 6);
        }

        public static bool CheckOutputNumber(int n)
        {
            return (n > 0 && n.ToString().Length <= 6);
        }

        public static bool IsCriminalCase(int caseKindID)
        {
            int[] set = {   2001,
                            2002,
                            2003,
                            2004,
                            2005,
                            2006,
                            2007,
                            2008,
                            2009
                        };

            return set.Contains(caseKindID);
        }

        public static bool IsAdministrativeCourt(int courtID)
        {
            int[] set = { 
                            701,
                            702,
                            703,
                            704,
                            705,
                            706,
                            707,
                            708,
                            709,
                            710,
                            711,
                            712,
                            713,
                            714,
                            715,
                            716,
                            717,
                            718,
                            719,
                            720,
                            721,
                            722,
                            723,
                            724,
                            725,
                            726,
                            727,
                            728
                        };

            return set.Contains(courtID);
        }
    }
}
