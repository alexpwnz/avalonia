namespace AvaloniaApplication1.Views
{
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Input;
    using Avalonia.Interactivity;

    public partial class UserView : UserControl
    {
        private bool _isDragging;
        private Point _lastPoint;

        public UserView()
        {
            InitializeComponent();
            AddHandler
            (
                PointerPressedEvent,
                OnPointerPressed, RoutingStrategies.Tunnel
            );
            AddHandler
            (
                PointerMovedEvent,
                OnPointerMoved, RoutingStrategies.Tunnel
            );
            AddHandler
            (
                PointerReleasedEvent,
                OnPointerReleased, RoutingStrategies.Tunnel
            );
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (!e.GetCurrentPoint(scrollViewer).Properties.IsLeftButtonPressed) return;
            _isDragging = true;
            _lastPoint = e.GetPosition(scrollViewer);
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (!_isDragging || !e.GetCurrentPoint(scrollViewer).Properties.IsLeftButtonPressed) return;
            var currentPoint = e.GetPosition(scrollViewer);
            scrollViewer.Offset = new Vector(scrollViewer.Offset.X + (_lastPoint.X - currentPoint.X),
                scrollViewer.Offset.Y);
            _lastPoint = currentPoint;
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _isDragging = false;
        }
    }
}