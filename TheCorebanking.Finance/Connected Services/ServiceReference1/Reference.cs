﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.CurrencyServiceSoap")]
    public interface CurrencyServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConvertCurrency", ReplyAction="*")]
        System.Threading.Tasks.Task<ServiceReference1.ConvertCurrencyResponse> ConvertCurrencyAsync(ServiceReference1.ConvertCurrencyRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ConvertCurrencyRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ConvertCurrency", Namespace="http://tempuri.org/", Order=0)]
        public ServiceReference1.ConvertCurrencyRequestBody Body;
        
        public ConvertCurrencyRequest()
        {
        }
        
        public ConvertCurrencyRequest(ServiceReference1.ConvertCurrencyRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ConvertCurrencyRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public decimal amount;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string fromCurrency;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string toCurrency;
        
        public ConvertCurrencyRequestBody()
        {
        }
        
        public ConvertCurrencyRequestBody(decimal amount, string fromCurrency, string toCurrency)
        {
            this.amount = amount;
            this.fromCurrency = fromCurrency;
            this.toCurrency = toCurrency;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ConvertCurrencyResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ConvertCurrencyResponse", Namespace="http://tempuri.org/", Order=0)]
        public ServiceReference1.ConvertCurrencyResponseBody Body;
        
        public ConvertCurrencyResponse()
        {
        }
        
        public ConvertCurrencyResponse(ServiceReference1.ConvertCurrencyResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ConvertCurrencyResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public decimal ConvertCurrencyResult;
        
        public ConvertCurrencyResponseBody()
        {
        }
        
        public ConvertCurrencyResponseBody(decimal ConvertCurrencyResult)
        {
            this.ConvertCurrencyResult = ConvertCurrencyResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface CurrencyServiceSoapChannel : ServiceReference1.CurrencyServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class CurrencyServiceSoapClient : System.ServiceModel.ClientBase<ServiceReference1.CurrencyServiceSoap>, ServiceReference1.CurrencyServiceSoap
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public CurrencyServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(CurrencyServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), CurrencyServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CurrencyServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(CurrencyServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CurrencyServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(CurrencyServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CurrencyServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.ConvertCurrencyResponse> ServiceReference1.CurrencyServiceSoap.ConvertCurrencyAsync(ServiceReference1.ConvertCurrencyRequest request)
        {
            return base.Channel.ConvertCurrencyAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.ConvertCurrencyResponse> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            ServiceReference1.ConvertCurrencyRequest inValue = new ServiceReference1.ConvertCurrencyRequest();
            inValue.Body = new ServiceReference1.ConvertCurrencyRequestBody();
            inValue.Body.amount = amount;
            inValue.Body.fromCurrency = fromCurrency;
            inValue.Body.toCurrency = toCurrency;
            return ((ServiceReference1.CurrencyServiceSoap)(this)).ConvertCurrencyAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.CurrencyServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.CurrencyServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.CurrencyServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost/financeconverter/CurrencyService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.CurrencyServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost/financeconverter/CurrencyService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            CurrencyServiceSoap,
            
            CurrencyServiceSoap12,
        }
    }
}
