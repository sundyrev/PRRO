using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PRRODriver;
using System.Windows.Forms;
using PRRODriver.Entities;

namespace MainApp
{
    public class MainVM : INotifyPropertyChanged
    {
        private readonly Driver driver;

        private string serverState;
        public string ServerState
        {
            get { return serverState; }
            set 
            {
                serverState = value;
                RaiseOnPropertyChanged();
            }
        }

        private string sertificatePath;
        public string SertificatePath
        {
            get { return sertificatePath; }
            set
            {
                sertificatePath = value;
                RaiseOnPropertyChanged();
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaiseOnPropertyChanged();
            }
        }

        public MainVM()
        {
            ServerState = "Unknown!";
            driver = new Driver();
        }

        #region Commands
        private DelegateCommand checkServerStateCommand;
        public ICommand CheckServerStateCommand
        {
            get { return checkServerStateCommand ?? (checkServerStateCommand = new DelegateCommand(CheckServerState)); }
        }
        private void CheckServerState()
        {
            ServerState = driver.GetServerState();
        }

        private DelegateCommand selectSertificateCommand;
        public ICommand SelectSertificateCommand
        {
            get { return selectSertificateCommand ?? (selectSertificateCommand = new DelegateCommand(SelectSertificate)); }
        }
        private void SelectSertificate()
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Certificates (*.cer)|*.cer";
                openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (openDialog.ShowDialog() != DialogResult.OK) return;

                SertificatePath = openDialog.FileName;
            }
        }

        private DelegateCommand getObjectsCommand;
        public ICommand GetObjectsCommand
        {
            get { return getObjectsCommand ?? (getObjectsCommand = new DelegateCommand(GetObjects, GetObjectsCanExecute)); }
        }
        private void GetObjects()
        {
            var someResult = driver.GetTaxObjects(new AccountInfo(SertificatePath, Password));
        }
        private bool GetObjectsCanExecute()
        {
            return !string.IsNullOrEmpty(SertificatePath) && !string.IsNullOrEmpty(Password);
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaiseOnPropertyChanged([CallerMemberName] string aName = null)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(aName));
        }
        #endregion
    }
}
