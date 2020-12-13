using System;

namespace ConsoleApp.Models
{
    public class RuntimeInformationContext
    {
        public string OperatingSystem { get; set; }

        public string ProcessArchitecture { get; set; }

        public RuntimeInformationContext()
        {
        }

        public RuntimeInformationContext(string operatingSystem, string processArchitecture)
        {
            OperatingSystem = operatingSystem;
            ProcessArchitecture = processArchitecture;
        }

        public bool Equals(RuntimeInformationContext other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return OperatingSystem == other.OperatingSystem
                && ProcessArchitecture == other.ProcessArchitecture;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RuntimeInformationContext)obj);
        }

        public override int GetHashCode() => HashCode.Combine(OperatingSystem, ProcessArchitecture);

        public override string ToString()
        {
            return $"{nameof(OperatingSystem)}={OperatingSystem} {nameof(ProcessArchitecture)}={ProcessArchitecture}";
        }
    }
}
