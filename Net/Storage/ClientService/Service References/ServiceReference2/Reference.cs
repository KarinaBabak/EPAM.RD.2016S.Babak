﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientService.ServiceReference2 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/UserStorage")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.DateTime DateOfBirthk__BackingFieldField;
        
        private string FirstNamek__BackingFieldField;
        
        private int Idk__BackingFieldField;
        
        private string LastNamek__BackingFieldField;
        
        private ClientService.ServiceReference2.Gender UserGenderk__BackingFieldField;
        
        private ClientService.ServiceReference2.VisaRecord[] VisaRecordsk__BackingFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<DateOfBirth>k__BackingField", IsRequired=true)]
        public System.DateTime DateOfBirthk__BackingField {
            get {
                return this.DateOfBirthk__BackingFieldField;
            }
            set {
                if ((this.DateOfBirthk__BackingFieldField.Equals(value) != true)) {
                    this.DateOfBirthk__BackingFieldField = value;
                    this.RaisePropertyChanged("DateOfBirthk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<FirstName>k__BackingField", IsRequired=true)]
        public string FirstNamek__BackingField {
            get {
                return this.FirstNamek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNamek__BackingFieldField, value) != true)) {
                    this.FirstNamek__BackingFieldField = value;
                    this.RaisePropertyChanged("FirstNamek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Id>k__BackingField", IsRequired=true)]
        public int Idk__BackingField {
            get {
                return this.Idk__BackingFieldField;
            }
            set {
                if ((this.Idk__BackingFieldField.Equals(value) != true)) {
                    this.Idk__BackingFieldField = value;
                    this.RaisePropertyChanged("Idk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<LastName>k__BackingField", IsRequired=true)]
        public string LastNamek__BackingField {
            get {
                return this.LastNamek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNamek__BackingFieldField, value) != true)) {
                    this.LastNamek__BackingFieldField = value;
                    this.RaisePropertyChanged("LastNamek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<UserGender>k__BackingField", IsRequired=true)]
        public ClientService.ServiceReference2.Gender UserGenderk__BackingField {
            get {
                return this.UserGenderk__BackingFieldField;
            }
            set {
                if ((this.UserGenderk__BackingFieldField.Equals(value) != true)) {
                    this.UserGenderk__BackingFieldField = value;
                    this.RaisePropertyChanged("UserGenderk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<VisaRecords>k__BackingField", IsRequired=true)]
        public ClientService.ServiceReference2.VisaRecord[] VisaRecordsk__BackingField {
            get {
                return this.VisaRecordsk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.VisaRecordsk__BackingFieldField, value) != true)) {
                    this.VisaRecordsk__BackingFieldField = value;
                    this.RaisePropertyChanged("VisaRecordsk__BackingField");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Gender", Namespace="http://schemas.datacontract.org/2004/07/UserStorage")]
    public enum Gender : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Female = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Male = 1,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VisaRecord", Namespace="http://schemas.datacontract.org/2004/07/UserStorage")]
    [System.SerializableAttribute()]
    public partial struct VisaRecord : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string Countryk__BackingFieldField;
        
        private System.DateTime EndDatek__BackingFieldField;
        
        private System.DateTime StartDatek__BackingFieldField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Country>k__BackingField", IsRequired=true)]
        public string Countryk__BackingField {
            get {
                return this.Countryk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Countryk__BackingFieldField, value) != true)) {
                    this.Countryk__BackingFieldField = value;
                    this.RaisePropertyChanged("Countryk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<EndDate>k__BackingField", IsRequired=true)]
        public System.DateTime EndDatek__BackingField {
            get {
                return this.EndDatek__BackingFieldField;
            }
            set {
                if ((this.EndDatek__BackingFieldField.Equals(value) != true)) {
                    this.EndDatek__BackingFieldField = value;
                    this.RaisePropertyChanged("EndDatek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<StartDate>k__BackingField", IsRequired=true)]
        public System.DateTime StartDatek__BackingField {
            get {
                return this.StartDatek__BackingFieldField;
            }
            set {
                if ((this.StartDatek__BackingFieldField.Equals(value) != true)) {
                    this.StartDatek__BackingFieldField = value;
                    this.RaisePropertyChanged("StartDatek__BackingField");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FemaleCriterion", Namespace="http://schemas.datacontract.org/2004/07/UserStorage.SearchCriteria")]
    [System.SerializableAttribute()]
    public partial class FemaleCriterion : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MaleCriterion", Namespace="http://schemas.datacontract.org/2004/07/UserStorage.SearchCriteria")]
    [System.SerializableAttribute()]
    public partial class MaleCriterion : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.IUserServiceContract")]
    public interface IUserServiceContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserServiceContract/Add", ReplyAction="http://tempuri.org/IUserServiceContract/AddResponse")]
        int Add(ClientService.ServiceReference2.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserServiceContract/Add", ReplyAction="http://tempuri.org/IUserServiceContract/AddResponse")]
        System.Threading.Tasks.Task<int> AddAsync(ClientService.ServiceReference2.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserServiceContract/Delete", ReplyAction="http://tempuri.org/IUserServiceContract/DeleteResponse")]
        void Delete(ClientService.ServiceReference2.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserServiceContract/Delete", ReplyAction="http://tempuri.org/IUserServiceContract/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(ClientService.ServiceReference2.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserServiceContract/SearchForUser", ReplyAction="http://tempuri.org/IUserServiceContract/SearchForUserResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ClientService.ServiceReference2.User))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ClientService.ServiceReference2.Gender))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ClientService.ServiceReference2.VisaRecord[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ClientService.ServiceReference2.VisaRecord))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ClientService.ServiceReference2.FemaleCriterion))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ClientService.ServiceReference2.MaleCriterion))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(int[]))]
        int[] SearchForUser(object[] criteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserServiceContract/SearchForUser", ReplyAction="http://tempuri.org/IUserServiceContract/SearchForUserResponse")]
        System.Threading.Tasks.Task<int[]> SearchForUserAsync(object[] criteria);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceContractChannel : ClientService.ServiceReference2.IUserServiceContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceContractClient : System.ServiceModel.ClientBase<ClientService.ServiceReference2.IUserServiceContract>, ClientService.ServiceReference2.IUserServiceContract {
        
        public UserServiceContractClient() {
        }
        
        public UserServiceContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int Add(ClientService.ServiceReference2.User user) {
            return base.Channel.Add(user);
        }
        
        public System.Threading.Tasks.Task<int> AddAsync(ClientService.ServiceReference2.User user) {
            return base.Channel.AddAsync(user);
        }
        
        public void Delete(ClientService.ServiceReference2.User user) {
            base.Channel.Delete(user);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(ClientService.ServiceReference2.User user) {
            return base.Channel.DeleteAsync(user);
        }
        
        public int[] SearchForUser(object[] criteria) {
            return base.Channel.SearchForUser(criteria);
        }
        
        public System.Threading.Tasks.Task<int[]> SearchForUserAsync(object[] criteria) {
            return base.Channel.SearchForUserAsync(criteria);
        }
    }
}
