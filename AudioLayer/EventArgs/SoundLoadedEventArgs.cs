namespace AudioLayer.EventArgs
{
    public sealed class SoundLoadedEventArgs : System.EventArgs
    {
        public string Soundname { get; init; }
        public SoundLoadedEventArgs(string soundname)
        {
            this.Soundname = soundname;
        }
    }
}
