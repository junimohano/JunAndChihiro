//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JunAndChihiro.Annotations;
using Xamarin.Forms;

namespace JunAndChihiro.Models
{
    public partial class JFile : INotifyPropertyChanged
    {

        private string _filePath;
        private string _name;
        private string _fileName;
        private string _description;
        private DateTime? _date;
        public System.Guid FileOid { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public Nullable<System.DateTime> Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public System.Guid CreatedOid { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> UpdatedOid { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FolderPath { get; set; }
        public Nullable<System.Guid> FolderOid { get; set; }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public byte[] Thumb { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
