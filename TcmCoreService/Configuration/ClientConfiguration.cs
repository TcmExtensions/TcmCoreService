#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Client Configuration
// ---------------------------------------------------------------------------------
//	Date Created	: March 19, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : 
//	Changed By          : 
//	Change Description  : 
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Configuration
{
	internal static class ClientConfiguration
	{
		private static BasicHttpBinding mClientHttpBinding;
		private static WSHttpBinding mClientWSHttpBinding;
		private static NetTcpBinding mClientTcpBinding;

		private static BasicHttpBinding mStreamHttpBinding;
		private static NetTcpBinding mStreamTcpBinding;

		internal static bool IsRunningOnMono
		{
			get
			{
				return Type.GetType ("Mono.Runtime") != null;
			}
		}

		internal static BasicHttpBinding ClientHttpBinding
		{
			get 
			{
				if (mClientHttpBinding == null)
				{
					try
					{
						// First try and load from a BasicHttpBinding configuration named "ClientHttp".
						mClientHttpBinding = new BasicHttpBinding("ClientHttp");
					}
					catch
					{
						// Configuration is not available, create a binding directly
						mClientHttpBinding = new BasicHttpBinding()
						{
							AllowCookies = true,
							BypassProxyOnLocal = false,
							UseDefaultWebProxy = false,
							MaxBufferPoolSize = 4 * 1048576, // 4 MB
							MaxReceivedMessageSize = 4 * 1048576, // 4 MB
							OpenTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							CloseTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							SendTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReceiveTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReaderQuotas = new XmlDictionaryReaderQuotas()
							{
								MaxStringContentLength = 4 * 1048576,
								MaxArrayLength = 4 * 1048576
							},
                            Security = new BasicHttpSecurity()
                            {
                                Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                                Transport = new HttpTransportSecurity() { ClientCredentialType = HttpClientCredentialType.Ntlm }
                            }                            
						};
					}
				}
	
				return mClientHttpBinding;
			}
		}

		internal static WSHttpBinding ClientWSHttpBinding
		{
			get
			{
				if (mClientWSHttpBinding == null)
				{
					try
					{
						// First try and load from a WSHttpBinding configuration named "WSClientHttp".
						mClientWSHttpBinding = new WSHttpBinding("WSClientHttp");
					}
					catch
					{
						// Configuration is not available, create a binding directly
						mClientWSHttpBinding = new WSHttpBinding()
						{
							AllowCookies = true,
							BypassProxyOnLocal = false,
							UseDefaultWebProxy = false,
							TransactionFlow = true,
							MaxBufferPoolSize = 4 * 1048576, // 4 MB
							MaxReceivedMessageSize = 4 * 1048576, // 4 MB
							OpenTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							CloseTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							SendTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReceiveTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReaderQuotas = new XmlDictionaryReaderQuotas()
							{
								MaxStringContentLength = 4 * 1048576,
								MaxArrayLength = 4 * 1048576
							},
						};
					}
				}

				mClientWSHttpBinding.Security.Mode = SecurityMode.Message;
				mClientWSHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;

				return mClientWSHttpBinding;
			}
		}

		internal static NetTcpBinding ClientTcpBinding
		{
			get
			{
				if (mClientTcpBinding == null)
				{
					try
					{
						// First try and load from a NetTcpBinding configuration named "ClientTcp".
						mClientTcpBinding = new NetTcpBinding("ClientTcp");
					}
					catch
					{
						// Configuration is not available, create a binding directly
						mClientTcpBinding = new NetTcpBinding()
						{
							TransactionFlow = true,
							MaxBufferPoolSize = 4 * 1048576, // 4 MB
							MaxReceivedMessageSize = 4 * 1048576, // 4 MB
							OpenTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							CloseTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							SendTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReceiveTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReaderQuotas = new XmlDictionaryReaderQuotas()
							{
								MaxStringContentLength = 4 * 1048576,
								MaxArrayLength = 4 * 1048576
							}
						};
					}
				}

				return mClientTcpBinding;
			}
		}

		internal static BasicHttpBinding StreamHttpBinding
		{
			get
			{
				if (mStreamHttpBinding == null)
				{
					try
					{
						// First try and load from a BasicHttpBinding configuration named "StreamHttp".
						mStreamHttpBinding = new BasicHttpBinding("StreamHttp");
					}
					catch
					{
						// Configuration is not available, create a binding directly
						mStreamHttpBinding = new BasicHttpBinding()
						{
							AllowCookies = true,
							BypassProxyOnLocal = false,
							UseDefaultWebProxy = false,
							MaxBufferPoolSize = 4 * 1048576, // 4 MB
							MaxReceivedMessageSize = 4 * 1048576, // 4 MB
							OpenTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							CloseTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							SendTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReceiveTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReaderQuotas = new XmlDictionaryReaderQuotas()
							{
								MaxStringContentLength = 4 * 1048576,
								MaxArrayLength = 4 * 1048576
							},
							MessageEncoding = WSMessageEncoding.Mtom
							/*The purpose for using MTOM (Message Transmission Optimization Mechanism) is to optimize the transmission of
								large binary data. Sending a SOAP message using MTOM has a noticeable overhead for small binary data,
								but becomes a great savings when they grow over a few thousand bytes. The reason for this is that normal text
								XML encodes binary data using Base64, which requires four characters for every three bytes, and increases the
								size of the data by one third. MTOM is able to transmit binary data as raw bytes, saving the encoding/decoding
								time and resulting is smaller messages.*/
						};
					}
				}

				return mStreamHttpBinding;
			}
		}

		internal static NetTcpBinding StreamTcpBinding
		{
			get
			{
				if (mStreamTcpBinding == null)
				{
					try
					{
						// First try and load from a NetTcpBinding configuration named "StreamTcp".
						mStreamTcpBinding = new NetTcpBinding("StreamTcp");
					}
					catch
					{
						// Configuration is not available, create a binding directly
						mStreamTcpBinding = new NetTcpBinding()
						{
							TransactionFlow = true,
							MaxBufferPoolSize = 4 * 1048576, // 4 MB
							MaxReceivedMessageSize = 4 * 1048576, // 4 MB
							OpenTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							CloseTimeout = TimeSpan.FromMinutes(2), // 2 minutes
							SendTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReceiveTimeout = TimeSpan.FromMinutes(10), // 10 minutes
							ReaderQuotas = new XmlDictionaryReaderQuotas()
							{
								MaxStringContentLength = 4 * 1048576,
								MaxArrayLength = 4 * 1048576
							}
						};
					}
				}

				return mStreamTcpBinding;
			}
		}

		internal static Uri ClientHttpUri(Uri targetUri)
		{
			return new UriBuilder()
			{
				Scheme = "http",
				Port = 80,
				Path = "/webservices/CoreService2013.svc/basicHttp",
				Host = targetUri.Host
			}.Uri;
		}

		internal static Uri ClientWSHttpUri(Uri targetUri)
		{
			return new UriBuilder()
			{
				Scheme = "http",
				Port = 80,
				Path = "/webservices/CoreService2013.svc/wsHttp",
				Host = targetUri.Host
			}.Uri;
		}

		internal static Uri ClientTcpUri(Uri targetUri)
		{
			return new UriBuilder()
			{
				Scheme = "net.tcp",
				Port = 2660,
				Path = "/CoreService/2013/netTcp",
				Host = targetUri.Host
			}.Uri;
		}

		internal static Uri UploadHttpUri(Uri targetUri)
		{
			return new UriBuilder()
			{
				Scheme = "http",
				Port = 80,
				Path = "/webservices/CoreService2011.svc/streamUpload_basicHttp",
				Host = targetUri.Host
			}.Uri;
		}

		internal static Uri UploadTcpUri(Uri targetUri)
		{
			return new UriBuilder()
			{
				Scheme = "net.tcp",
				Port = 2660,
				Path = "/CoreService/2011/streamUpload_netTcp",
				Host = targetUri.Host
			}.Uri;
		}

		internal static Uri DownloadHttpUri(Uri targetUri)
		{
			return new UriBuilder()
			{
				Scheme = "http",
				Port = 80,
				Path = "/webservices/CoreService2011.svc/streamDownload_basicHttp",
				Host = targetUri.Host
			}.Uri;
		}

		internal static Uri DownloadTcpUri(Uri targetUri)
		{
			return new UriBuilder()
			{
				Scheme = "net.tcp",
				Port = 2660,
				Path = "/CoreService/2011/streamDownload_netTcp",
				Host = targetUri.Host
			}.Uri;
		}
	}
}
