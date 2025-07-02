namespace NcvibJson.Triggered.V0_1
{
    public class TransientNcvibJson
    {
        public string FormatVersion { get; } = "1";
        public required string SensorCompany { get; set; }
        public required string SensorType { get; set; }
        public int SerialNumber { get; set; }
        public int? Port { get; set; }
        public DateTimeOffset Time { get; set; }
        public required string MeasuredEntity { get; set; }
        public List<double> MeasuredMaxValue { get; set; } = new List<double>();
        public required string MeasuredUnit { get; set; }

        public required string HashId { get; set; }

        public List<double> Coordinate { get; set; } = new List<double>();
        public double? Battery { get; set; }
        public double? Temperature { get; set; }
        public int? StandardFilterId { get; set; }
        public bool FilteredSamples { get; set; } = true;
        public int SampleRate { get; set; }
        public int NumberOfPreTrigSamples { get; set; }

        public List<double> PeakParticleVelocity { get; set; } = new List<double>();
        public float? VectorPeakParticleVelocity { get; set; }
        public required string PeakParticleVelocityUnit { get; set; }

        public List<double> PeakParticleAcceleration { get; set; } = new List<double>();
        public required string PeakParticleAccelerationUnit { get; set; }

        public List<double> PeakParticleDisplacement { get; set; } = new List<double>();
        public required string PeakParticleDisplacementUnit { get; set; }

        public List<double> ZeroCuttingFrequencyVelocity { get; set; } = new List<double>();
        public List<double> ZeroCuttingFrequencyAcceleration { get; set; } = new List<double>();

        public double SoundLevelMax { get; set; }
        public double SoundLevelAverage { get; set; }

        public double VibrationLevelMax { get; set; }
        public double VibrationLevelTop10 { get; set; }

        public bool Overload { get; set; }

        public List<string> VibrationAxes { get; set; } = new List<string>();
        public List<string> Units { get; set; } = new List<string>();
        public List<List<double>> Samples { get; set; } = new List<List<double>>();

        public bool MeasuresAnythingButSoundOrVibration => MeasuredEntity != MeasureEntity.VibrationLevel && MeasuredEntity != MeasureEntity.SoundLevel;
        public bool MeasuresSound => MeasuredEntity == MeasureEntity.SoundLevel;
        public bool MeasuresVibration => MeasuredEntity == MeasureEntity.VibrationLevel;
    }
}