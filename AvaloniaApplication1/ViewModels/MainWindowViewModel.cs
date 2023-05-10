namespace AvaloniaApplication1.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Reactive;
    using ReactiveUI;

    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ButtonViewModel> _buttons = new();

        public MainWindowViewModel()
        {
            AddButton = ReactiveCommand.Create(AddButtonImpl);
            RemoveButton = ReactiveCommand.Create(RemoveButtonImpl,
                this.WhenAnyValue(q => q.Buttons.Count, count => count > 0));
        }


        public ObservableCollection<ButtonViewModel> Buttons
        {
            get => _buttons;
            set => this.RaiseAndSetIfChanged(ref _buttons, value);
        }

        public ReactiveCommand<Unit, Unit> AddButton { get; }
        public ReactiveCommand<Unit, Unit> RemoveButton { get; }

        private void AddButtonImpl()
        {
            _buttons.Add(new ButtonViewModel());
        }

        private void RemoveButtonImpl()
        {
            _buttons.RemoveAt(0);
        }
    }
}