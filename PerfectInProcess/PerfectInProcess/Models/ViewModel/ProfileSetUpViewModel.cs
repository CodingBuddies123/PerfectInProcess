using System;

namespace PerfectInProcess.Models.ViewModel
{
    public class ProfileSetUpViewModel : BaseViewModel
    {
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int HeartRate { get; set; }
    }
}