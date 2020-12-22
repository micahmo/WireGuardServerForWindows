﻿using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WireGuardServerForWindows.Models
{
    public abstract class PrerequisiteItem : ObservableObject
    {
        #region Constructor

        protected PrerequisiteItem()
        {
            Commands = new PrerequisiteItemCommands(this);
        }

        #endregion

        #region Public properties

        public PrerequisiteItemCommands Commands { get; }

        public string Title
        {
            get => _title;
            set => Set(nameof(Title), ref _title, value);
        }
        private string _title;

        public bool Fulfilled
        {
            get => _fulfilled;
            set => Set(nameof(Fulfilled), ref _fulfilled, value);
        }
        private bool _fulfilled;

        public string SuccessMessage
        {
            get => _successMessage;
            set => Set(nameof(SuccessMessage), ref _successMessage, value);
        }
        private string _successMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(nameof(ErrorMessage), ref _errorMessage, value);
        }
        private string _errorMessage;

        public string ResolveText
        {
        get => _resolveText;
            set => Set(nameof(ResolveText), ref _resolveText, value);
        }
        private string _resolveText;

        public string ConfigureText
        {
            get => _configureText;
            set => Set(nameof(ConfigureText), ref _configureText, value);
        }
        private string _configureText;

        #endregion

        #region Public methods

        public abstract void Resolve();

        public abstract void Configure();

        public abstract void Refresh();

        #endregion
    }

    public class PrerequisiteItemCommands
    {
        public PrerequisiteItemCommands(PrerequisiteItem prerequisiteItem)
        {
            PrerequisiteItem = prerequisiteItem;
        }

        private PrerequisiteItem PrerequisiteItem { get; }

        #region ICommands

        public ICommand ResolveCommand => _resolveCommand ??= new RelayCommand(PrerequisiteItem.Resolve);
        private RelayCommand _resolveCommand;

        public ICommand ConfigureCommand => _configureCommand ??= new RelayCommand(PrerequisiteItem.Configure);
        private RelayCommand _configureCommand;

        #endregion

        #region Command implementations

        #endregion
    }
}