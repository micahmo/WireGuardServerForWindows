﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using WireGuardServerForWindows.Properties;

namespace WireGuardServerForWindows.Models
{
    public class ClientConfiguration : ConfigurationBase
    {
        #region Constructor

        public ClientConfiguration(ClientConfigurationList parentList)
        {
            _parentList = parentList;

            // Client properties
            PrivateKeyProperty.TargetTypes.Add(GetType());

            // Server properties
            PresharedKeyProperty.TargetTypes.Add(typeof(ClientConfiguration));
            PublicKeyProperty.TargetTypes.Add(typeof(ClientConfiguration));

            // Add support for deleting client config (not supported in server)
            NameProperty.Action = new ConfigurationPropertyAction
            {
                Name = Resources.Delete,
                Action = (conf, prop) =>
                {
                    MessageBoxResult res = MessageBox.Show(Resources.ConfirmDeleteClient, Resources.Confirm, MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        (conf as ClientConfiguration)?.RemoveClientCommand?.Execute(null);
                    }
                }
            };
        }

        #endregion

        #region Public properties

        public ICommand RemoveClientCommand => _removeClientCommand ??= new RelayCommand(() =>
        {
            using (new WaitCursor(dispatcherPriority: DispatcherPriority.Render, restoreCursorToNull: true))
            {
                _parentList?.List.Remove(this);
            }
        });
        private RelayCommand _removeClientCommand;

        #endregion

        #region Private fields

        private readonly ClientConfigurationList _parentList;

        #endregion
    }
}