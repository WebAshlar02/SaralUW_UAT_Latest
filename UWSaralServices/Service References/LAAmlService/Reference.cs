﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UWSaralServices.LAAmlService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MasterClient", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    public partial class MasterClient : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ERRORCODEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FILLERField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string VALUESField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ERRORCODE {
            get {
                return this.ERRORCODEField;
            }
            set {
                if ((object.ReferenceEquals(this.ERRORCODEField, value) != true)) {
                    this.ERRORCODEField = value;
                    this.RaisePropertyChanged("ERRORCODE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FILLER {
            get {
                return this.FILLERField;
            }
            set {
                if ((object.ReferenceEquals(this.FILLERField, value) != true)) {
                    this.FILLERField = value;
                    this.RaisePropertyChanged("FILLER");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VALUES {
            get {
                return this.VALUESField;
            }
            set {
                if ((object.ReferenceEquals(this.VALUESField, value) != true)) {
                    this.VALUESField = value;
                    this.RaisePropertyChanged("VALUES");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LAAmlService.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AMLCRT", ReplyAction="http://tempuri.org/IService/AMLCRTResponse")]
        UWSaralServices.LAAmlService.MasterClient AMLCRT(
                    string CLTTWO, 
                    string ADDRPRF, 
                    string CLNTRSKIND, 
                    string IDPRF, 
                    string IDPRFDT, 
                    string IDPRFNUM, 
                    string INCPRF, 
                    string ISSUEAUTH, 
                    string REASONCD, 
                    string ZAGEPRF, 
                    string ZFOTOIND, 
                    string ZFOTOPRF, 
                    string ZPANIDNO, 
                    string ZFORM, 
                    string ZPANCOPY, 
                    string Branch, 
                    string UserRole, 
                    string UserID, 
                    string PartnerID, 
                    string ProcessID, 
                    string ApplicationNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AMLCRT", ReplyAction="http://tempuri.org/IService/AMLCRTResponse")]
        System.Threading.Tasks.Task<UWSaralServices.LAAmlService.MasterClient> AMLCRTAsync(
                    string CLTTWO, 
                    string ADDRPRF, 
                    string CLNTRSKIND, 
                    string IDPRF, 
                    string IDPRFDT, 
                    string IDPRFNUM, 
                    string INCPRF, 
                    string ISSUEAUTH, 
                    string REASONCD, 
                    string ZAGEPRF, 
                    string ZFOTOIND, 
                    string ZFOTOPRF, 
                    string ZPANIDNO, 
                    string ZFORM, 
                    string ZPANCOPY, 
                    string Branch, 
                    string UserRole, 
                    string UserID, 
                    string PartnerID, 
                    string ProcessID, 
                    string ApplicationNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AMLUPD", ReplyAction="http://tempuri.org/IService/AMLUPDResponse")]
        UWSaralServices.LAAmlService.MasterClient AMLUPD(
                    string CLTTWO, 
                    string ADDRPRF, 
                    string CLNTRSKIND, 
                    string IDPRF, 
                    string IDPRFDT, 
                    string IDPRFNUM, 
                    string INCPRF, 
                    string ISSUEAUTH, 
                    string REASONCD, 
                    string ZAGEPRF, 
                    string ZFOTOIND, 
                    string ZFOTOPRF, 
                    string ZPANIDNO, 
                    string ZFORM, 
                    string ZPANCOPY, 
                    string Branch, 
                    string UserRole, 
                    string UserID, 
                    string PartnerID, 
                    string ProcessID, 
                    string ApplicationNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AMLUPD", ReplyAction="http://tempuri.org/IService/AMLUPDResponse")]
        System.Threading.Tasks.Task<UWSaralServices.LAAmlService.MasterClient> AMLUPDAsync(
                    string CLTTWO, 
                    string ADDRPRF, 
                    string CLNTRSKIND, 
                    string IDPRF, 
                    string IDPRFDT, 
                    string IDPRFNUM, 
                    string INCPRF, 
                    string ISSUEAUTH, 
                    string REASONCD, 
                    string ZAGEPRF, 
                    string ZFOTOIND, 
                    string ZFOTOPRF, 
                    string ZPANIDNO, 
                    string ZFORM, 
                    string ZPANCOPY, 
                    string Branch, 
                    string UserRole, 
                    string UserID, 
                    string PartnerID, 
                    string ProcessID, 
                    string ApplicationNo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : UWSaralServices.LAAmlService.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<UWSaralServices.LAAmlService.IService>, UWSaralServices.LAAmlService.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public UWSaralServices.LAAmlService.MasterClient AMLCRT(
                    string CLTTWO, 
                    string ADDRPRF, 
                    string CLNTRSKIND, 
                    string IDPRF, 
                    string IDPRFDT, 
                    string IDPRFNUM, 
                    string INCPRF, 
                    string ISSUEAUTH, 
                    string REASONCD, 
                    string ZAGEPRF, 
                    string ZFOTOIND, 
                    string ZFOTOPRF, 
                    string ZPANIDNO, 
                    string ZFORM, 
                    string ZPANCOPY, 
                    string Branch, 
                    string UserRole, 
                    string UserID, 
                    string PartnerID, 
                    string ProcessID, 
                    string ApplicationNo) {
            return base.Channel.AMLCRT(CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, ZFORM, ZPANCOPY, Branch, UserRole, UserID, PartnerID, ProcessID, ApplicationNo);
        }
        
        public System.Threading.Tasks.Task<UWSaralServices.LAAmlService.MasterClient> AMLCRTAsync(
                    string CLTTWO, 
                    string ADDRPRF, 
                    string CLNTRSKIND, 
                    string IDPRF, 
                    string IDPRFDT, 
                    string IDPRFNUM, 
                    string INCPRF, 
                    string ISSUEAUTH, 
                    string REASONCD, 
                    string ZAGEPRF, 
                    string ZFOTOIND, 
                    string ZFOTOPRF, 
                    string ZPANIDNO, 
                    string ZFORM, 
                    string ZPANCOPY, 
                    string Branch, 
                    string UserRole, 
                    string UserID, 
                    string PartnerID, 
                    string ProcessID, 
                    string ApplicationNo) {
            return base.Channel.AMLCRTAsync(CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, ZFORM, ZPANCOPY, Branch, UserRole, UserID, PartnerID, ProcessID, ApplicationNo);
        }
        
        public UWSaralServices.LAAmlService.MasterClient AMLUPD(
                    string CLTTWO, 
                    string ADDRPRF, 
                    string CLNTRSKIND, 
                    string IDPRF, 
                    string IDPRFDT, 
                    string IDPRFNUM, 
                    string INCPRF, 
                    string ISSUEAUTH, 
                    string REASONCD, 
                    string ZAGEPRF, 
                    string ZFOTOIND, 
                    string ZFOTOPRF, 
                    string ZPANIDNO, 
                    string ZFORM, 
                    string ZPANCOPY, 
                    string Branch, 
                    string UserRole, 
                    string UserID, 
                    string PartnerID, 
                    string ProcessID, 
                    string ApplicationNo) {
            return base.Channel.AMLUPD(CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, ZFORM, ZPANCOPY, Branch, UserRole, UserID, PartnerID, ProcessID, ApplicationNo);
        }
        
        public System.Threading.Tasks.Task<UWSaralServices.LAAmlService.MasterClient> AMLUPDAsync(
                    string CLTTWO, 
                    string ADDRPRF, 
                    string CLNTRSKIND, 
                    string IDPRF, 
                    string IDPRFDT, 
                    string IDPRFNUM, 
                    string INCPRF, 
                    string ISSUEAUTH, 
                    string REASONCD, 
                    string ZAGEPRF, 
                    string ZFOTOIND, 
                    string ZFOTOPRF, 
                    string ZPANIDNO, 
                    string ZFORM, 
                    string ZPANCOPY, 
                    string Branch, 
                    string UserRole, 
                    string UserID, 
                    string PartnerID, 
                    string ProcessID, 
                    string ApplicationNo) {
            return base.Channel.AMLUPDAsync(CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, ZFORM, ZPANCOPY, Branch, UserRole, UserID, PartnerID, ProcessID, ApplicationNo);
        }
    }
}
