using System.Windows;

namespace MaterialForms
{
    public static class SessionAssist
    {
        public static readonly DependencyProperty HostingSessionProperty =
            DependencyProperty.RegisterAttached(
                "HostingSession",
                typeof(Session),
                typeof(SessionAssist),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
    }
}