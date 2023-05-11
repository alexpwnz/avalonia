namespace AvaloniaApplication1.Views
{
    using System;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Input;
    using Avalonia.Interactivity;
    using Avalonia.Threading;

    public partial class UserView : UserControl
    {
        private readonly DispatcherTimer _inertiaTimer;
        private bool _isDragging;
        private Point _lastPoint;
        private Vector _velocity;

        public UserView()
        {
            InitializeComponent();
            AddHandler(PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel);
            AddHandler(PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel);
            AddHandler(PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel);

            _inertiaTimer = new DispatcherTimer();
            _inertiaTimer.Tick += OnInertiaTimerTick;
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (!e.GetCurrentPoint(scrollViewer).Properties.IsLeftButtonPressed)
                return;

            _isDragging = true;
            _lastPoint = e.GetPosition(scrollViewer);
            _inertiaTimer.Stop();

            e.Handled = true;
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (!_isDragging || !e.GetCurrentPoint(scrollViewer).Properties.IsLeftButtonPressed)
                return;

            var currentPoint = e.GetPosition(scrollViewer);

            scrollViewer.Offset = new Vector(scrollViewer.Offset.X + (_lastPoint.X - currentPoint.X),
                scrollViewer.Offset.Y);

            _velocity = (_velocity + (currentPoint - _lastPoint)) * 0.5;
            _lastPoint = currentPoint;


            e.Handled = true;
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _isDragging = false;

            _inertiaTimer.Interval = TimeSpan.FromSeconds(0.01);
            _inertiaTimer.Start();
        }

        private void OnInertiaTimerTick(object? sender, EventArgs e)
        {
            if (_velocity.Length < 0.1)
            {
                _inertiaTimer.Stop();
            }
            else
            {
                scrollViewer.Offset -= _velocity;

                _velocity *= 0.95;
            }
        }
    }
}
