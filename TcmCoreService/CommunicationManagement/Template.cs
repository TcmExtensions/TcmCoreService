#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Template
// ---------------------------------------------------------------------------------
//	Date Created	: March 21, 2014
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
using System.Text;
using TcmCoreService.ContentManagement;
using TcmCoreService.Info;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="Template" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TemplateData" />.
	/// </summary>
	public abstract class Template : VersionedItem
	{
		private TemplateData mTemplateData;
		private BinaryContent mBinaryContent = null;
		private Schema mParameterSchema = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Template"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="templateData"><see cref="T:Tridion.ContentManager.CoreService.Client.TemplateData" /></param>
		protected Template(Client client, TemplateData templateData): base(client, templateData)
		{
			if (templateData == null)
				throw new ArgumentNullException("templateData");

			mTemplateData = templateData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Template"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Template(Client client, TcmUri uri): this(client, client.Read<TemplateData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Template" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.TemplateData" />
		/// </summary>
		/// <param name="templateData"><see cref="T:Tridion.ContentManager.CoreService.Client.TemplateData" /></param>
		protected void Reload(TemplateData templateData)
		{
			if (templateData == null)
				throw new ArgumentNullException("templateData");

			mTemplateData = templateData;
			base.Reload(templateData);

			mBinaryContent = null;
			mParameterSchema = null;
		}

		/// <summary>
		/// Reload the <see cref="Template" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<TemplateData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="Template" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<TemplateData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="Template" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<TemplateData>(this.Id));
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Info.BinaryContent" /> for this <see cref="Template" />.
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.BinaryContent" /> for this <see cref="Template" />
		/// </value>
		public BinaryContent BinaryContent
		{
			get
			{
				if (mBinaryContent == null && mTemplateData.BinaryContent != null)
					mBinaryContent = new BinaryContent(Client, mTemplateData.BinaryContent);

				return mBinaryContent;
			}
			set
			{
				mBinaryContent = value;
				mTemplateData.BinaryContent = mBinaryContent.BinaryContentData;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Template" /> content.
		/// </summary>
		/// <value>
		/// The <see cref="Template" /> content.
		/// </value>
		public String Content
		{
			get
			{
				return mTemplateData.Content;
			}
			set
			{
				mTemplateData.Content = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Template" /> parameter <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// <see cref="Template" /> parameter <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema ParameterSchema
		{
			get
			{
				if (mParameterSchema == null && mTemplateData.ParameterSchema.IdRef != TcmUri.NullUri)
					mParameterSchema = new Schema(Client, mTemplateData.ParameterSchema.IdRef);

				return mParameterSchema;
			}
			set
			{
				mParameterSchema = value;

				if (value != null)
					mTemplateData.ParameterSchema.IdRef = value.Id;
				else
					mTemplateData.ParameterSchema.IdRef = TcmUri.NullUri;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Template" /> type
		/// </summary>
		/// <value>
		/// <see cref="Template"/> type
		/// </value>
		public String TemplateType
		{
			get
			{
				return mTemplateData.TemplateType;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mTemplateData.TemplateType = value;
			}
		}
	}
}
