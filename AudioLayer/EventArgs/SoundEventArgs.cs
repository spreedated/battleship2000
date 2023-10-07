namespace AudioLayer.EventArgs
{
    public sealed class SoundEventArgs : System.EventArgs
    {
        public string Soundname { get; init; }
        public SoundEventArgs(string soundname)
        {
            this.Soundname = soundname;
        }
    }
}
