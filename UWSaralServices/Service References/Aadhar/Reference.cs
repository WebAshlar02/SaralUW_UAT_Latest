﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UWSaralServices.Aadhar {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Aadhar.IeKYCService")]
    public interface IeKYCService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/Demographic", ReplyAction="http://tempuri.org/IeKYCService/DemographicResponse")]
        System.Data.DataSet Demographic(string username, string password, string ApplicationNo, string RequestSource, string requesttype, string Uid, string name, string gender, string dob);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/Demographic", ReplyAction="http://tempuri.org/IeKYCService/DemographicResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> DemographicAsync(string username, string password, string ApplicationNo, string RequestSource, string requesttype, string Uid, string name, string gender, string dob);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/OTPValidation", ReplyAction="http://tempuri.org/IeKYCService/OTPValidationResponse")]
        System.Data.DataSet OTPValidation(string username, string password, string ApplicationNo, string RequestSource, string requesttype, string Uid, string OTP, string txn);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/OTPValidation", ReplyAction="http://tempuri.org/IeKYCService/OTPValidationResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> OTPValidationAsync(string username, string password, string ApplicationNo, string RequestSource, string requesttype, string Uid, string OTP, string txn);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/GenerateOTP", ReplyAction="http://tempuri.org/IeKYCService/GenerateOTPResponse")]
        System.Data.DataSet GenerateOTP(string username, string password, string ApplicationNo, string RequestSource, string Uid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/GenerateOTP", ReplyAction="http://tempuri.org/IeKYCService/GenerateOTPResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> GenerateOTPAsync(string username, string password, string ApplicationNo, string RequestSource, string Uid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/Biometric", ReplyAction="http://tempuri.org/IeKYCService/BiometricResponse")]
        System.Data.DataSet Biometric(string username, string password, string ApplicationNo, string RequestSource, string Uid, string BiometricBytes, string fingerdetails);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/Biometric", ReplyAction="http://tempuri.org/IeKYCService/BiometricResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> BiometricAsync(string username, string password, string ApplicationNo, string RequestSource, string Uid, string BiometricBytes, string fingerdetails);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/BioMobile", ReplyAction="http://tempuri.org/IeKYCService/BioMobileResponse")]
        System.Data.DataSet BioMobile(
                    string username, 
                    string password, 
                    string ApplicationNo, 
                    string RequestSource, 
                    string Uid, 
                    string PIDData, 
                    string Hmac, 
                    string Skey, 
                    string biotypes, 
                    string timestamp, 
                    string residentconsent, 
                    string dpId, 
                    string rdsId, 
                    string rdsVer, 
                    string dc, 
                    string mi, 
                    string mc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IeKYCService/BioMobile", ReplyAction="http://tempuri.org/IeKYCService/BioMobileResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> BioMobileAsync(
                    string username, 
                    string password, 
                    string ApplicationNo, 
                    string RequestSource, 
                    string Uid, 
                    string PIDData, 
                    string Hmac, 
                    string Skey, 
                    string biotypes, 
                    string timestamp, 
                    string residentconsent, 
                    string dpId, 
                    string rdsId, 
                    string rdsVer, 
                    string dc, 
                    string mi, 
                    string mc);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IeKYCServiceChannel : UWSaralServices.Aadhar.IeKYCService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IeKYCServiceClient : System.ServiceModel.ClientBase<UWSaralServices.Aadhar.IeKYCService>, UWSaralServices.Aadhar.IeKYCService {
        
        public IeKYCServiceClient() {
        }
        
        public IeKYCServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IeKYCServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IeKYCServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IeKYCServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet Demographic(string username, string password, string ApplicationNo, string RequestSource, string requesttype, string Uid, string name, string gender, string dob) {
            return base.Channel.Demographic(username, password, ApplicationNo, RequestSource, requesttype, Uid, name, gender, dob);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> DemographicAsync(string username, string password, string ApplicationNo, string RequestSource, string requesttype, string Uid, string name, string gender, string dob) {
            return base.Channel.DemographicAsync(username, password, ApplicationNo, RequestSource, requesttype, Uid, name, gender, dob);
        }
        
        public System.Data.DataSet OTPValidation(string username, string password, string ApplicationNo, string RequestSource, string requesttype, string Uid, string OTP, string txn) {
            return base.Channel.OTPValidation(username, password, ApplicationNo, RequestSource, requesttype, Uid, OTP, txn);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> OTPValidationAsync(string username, string password, string ApplicationNo, string RequestSource, string requesttype, string Uid, string OTP, string txn) {
            return base.Channel.OTPValidationAsync(username, password, ApplicationNo, RequestSource, requesttype, Uid, OTP, txn);
        }
        
        public System.Data.DataSet GenerateOTP(string username, string password, string ApplicationNo, string RequestSource, string Uid) {
            return base.Channel.GenerateOTP(username, password, ApplicationNo, RequestSource, Uid);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GenerateOTPAsync(string username, string password, string ApplicationNo, string RequestSource, string Uid) {
            return base.Channel.GenerateOTPAsync(username, password, ApplicationNo, RequestSource, Uid);
        }
        
        public System.Data.DataSet Biometric(string username, string password, string ApplicationNo, string RequestSource, string Uid, string BiometricBytes, string fingerdetails) {
            return base.Channel.Biometric(username, password, ApplicationNo, RequestSource, Uid, BiometricBytes, fingerdetails);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> BiometricAsync(string username, string password, string ApplicationNo, string RequestSource, string Uid, string BiometricBytes, string fingerdetails) {
            return base.Channel.BiometricAsync(username, password, ApplicationNo, RequestSource, Uid, BiometricBytes, fingerdetails);
        }
        
        public System.Data.DataSet BioMobile(
                    string username, 
                    string password, 
                    string ApplicationNo, 
                    string RequestSource, 
                    string Uid, 
                    string PIDData, 
                    string Hmac, 
                    string Skey, 
                    string biotypes, 
                    string timestamp, 
                    string residentconsent, 
                    string dpId, 
                    string rdsId, 
                    string rdsVer, 
                    string dc, 
                    string mi, 
                    string mc) {
            return base.Channel.BioMobile(username, password, ApplicationNo, RequestSource, Uid, PIDData, Hmac, Skey, biotypes, timestamp, residentconsent, dpId, rdsId, rdsVer, dc, mi, mc);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> BioMobileAsync(
                    string username, 
                    string password, 
                    string ApplicationNo, 
                    string RequestSource, 
                    string Uid, 
                    string PIDData, 
                    string Hmac, 
                    string Skey, 
                    string biotypes, 
                    string timestamp, 
                    string residentconsent, 
                    string dpId, 
                    string rdsId, 
                    string rdsVer, 
                    string dc, 
                    string mi, 
                    string mc) {
            return base.Channel.BioMobileAsync(username, password, ApplicationNo, RequestSource, Uid, PIDData, Hmac, Skey, biotypes, timestamp, residentconsent, dpId, rdsId, rdsVer, dc, mi, mc);
        }
    }
}
