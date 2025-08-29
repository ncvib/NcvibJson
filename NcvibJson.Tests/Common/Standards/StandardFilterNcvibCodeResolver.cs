namespace NcvibJson.Tests.Common.Standards;

public class StandardFilterNcvibCodeResolver : IStandardFilterNcvibCodeResolver
{
    public int GetStandardFilterCode(string standardFilter)
    {
        return standardFilter switch
        {
            "NoFilter" => 0,
            "ISEESeismograph" => 1, // ISEE Seismograph
            "DIN41503" => 2,  // DIN4150-3 Anlage 1-315Hz
            "DIN41502KB" => 3, // DIN 4150-2 KB 1-80Hz
            "BS7385" => 4,  // BS 7385
            "AS21872_2006" => 5,  // AS 2187-2 2006
            "NORMS9012" => 6,  // ÖNORM S 9012
            "ISO8569Accel" => 7,  // Acceleration 5-300Hz
            "IN1226" => 8,  // IN1226
            "NS8176Komfort" => 9,  // NS 8176 Komfort 20mm/s 1-80Hz
            "NS8141Byggverk" => 10,  // NS 8141:2001 Byggverk 5-300Hz
            "NS8141_1_2012_A1_2013" => 11,  // NS 8141:2013 Byggverk 3-400Hz
            "SS4604866Sprang" => 12,  // SS 4604866 Spräng 5-300Hz
            "SS025211Schakt" => 13,  // SS 25211 Schakt 2-150Hz
            "SS4604861Komfort" => 14,  // SS 4604861 Komfort 1-80Hz
            "Geophone" => 15,  // Geophone 5-500Hz
            "ICPECirculaire86" => 0,  // Not in original mapping
            "SS25211Schakt5_150Hz" => 17,  // SS 25211 Schakt 5-150Hz
            _ => 0  // Default to Unknown
        };
    }    
}