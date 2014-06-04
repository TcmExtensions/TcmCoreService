﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Schema
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
using TcmCoreService.Misc;
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="Schema" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.SchemaData" />.
	/// </summary>
	public class Schema : VersionedItem
	{
		private SchemaData mSchemaData;

		private IEnumerable<MultimediaType> mAllowedMultimediaTypes = null;
		private ProcessDefinition mComponentProcess = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Schema"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="schemaData"><see cref="T:Tridion.ContentManager.CoreService.Client.SchemaData" /></param>
		protected Schema(Session session, SchemaData schemaData): base(session, schemaData)
		{
			if (schemaData == null)
				throw new ArgumentNullException("schemaData");

			mSchemaData = schemaData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Schema"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Schema(Session session, TcmUri uri): this(session, session.Read<SchemaData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Schema" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.SchemaData" />
		/// </summary>
		/// <param name="schemaData"><see cref="T:Tridion.ContentManager.CoreService.Client.SchemaData" /></param>
		protected void Reload(SchemaData schemaData)
		{
			if (schemaData == null)
				throw new ArgumentNullException("schemaData");

			mSchemaData = schemaData;
			base.Reload(schemaData);

			mAllowedMultimediaTypes = null;
			mComponentProcess = null;
		}

		/// <summary>
		/// Reload the <see cref="Schema" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<SchemaData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="Schema" />
		/// </summary>
		public override void Localize()
		{
			Reload(Session.Localize<SchemaData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="Schema" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Session.UnLocalize<SchemaData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets the <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.ContentManagement.MultimediaType" /> for this <see cref="Schema" />
		/// </summary>
		/// <value>
		/// <see cref="Schema" /> allowed <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.ContentManagement.MultimediaType" /> for this <see cref="Schema" />
		/// </value>
		public IEnumerable<MultimediaType> AllowedMultimediaTypes
		{
			get
			{
				if (mAllowedMultimediaTypes == null && mSchemaData.AllowedMultimediaTypes != null)
					mAllowedMultimediaTypes = mSchemaData.AllowedMultimediaTypes.Select(multimediaType => new MultimediaType(Session, multimediaType.IdRef));

				return mAllowedMultimediaTypes;				
			}
			set
			{
				mAllowedMultimediaTypes = value;

				if (mAllowedMultimediaTypes != null)
					mSchemaData.AllowedMultimediaTypes = mAllowedMultimediaTypes.Select(multimediaType => new LinkToMultimediaTypeData()
					{
						IdRef = multimediaType.Id
					}).ToArray();
				else
					mSchemaData.AllowedMultimediaTypes = null;				
			}
		}

		/// <summary>
		/// Gets or sets the component <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Schema" />
		/// </summary>
		/// <value>
		/// Component <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Schema" />
		/// </value>
		public ProcessDefinition ComponentProcess
		{
			get
			{
				if (mComponentProcess == null && mSchemaData.ComponentProcess != null)
					mComponentProcess = new TridionProcessDefinition(Session, mSchemaData.ComponentProcess.IdRef);

				return mComponentProcess;
			}
			set
			{
				mComponentProcess = value;

				if (value != null)
					mSchemaData.ComponentProcess.IdRef = value.Id;
				else
					mSchemaData.ComponentProcess.IdRef = TcmUri.NullUri;				
			}
		}

		/// <summary>
		/// Gets or sets <see cref="Schema" /> description
		/// </summary>
		/// <value>
		/// <see cref="Schema" /> description.
		/// </value>
		public String Description
		{
			get
			{
				return mSchemaData.Description;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mSchemaData.Description = value;				
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Schema" /> is a tridion web schema.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Schema" /> is a tridion web schema; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsTridionWebSchema
		{
			get
			{
				return mSchemaData.IsTridionWebSchema.GetValueOrDefault(false);
			}
			set
			{
				mSchemaData.IsTridionWebSchema = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Schema" /> namespace URI
		/// </summary>
		/// <value>
		/// <see cref="Schema" /> namespace URI.
		/// </value>
		public String NamespaceUri
		{
			get
			{
				return mSchemaData.NamespaceUri;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mSchemaData.NamespaceUri = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Schema" /> <see cref="T:Tridion.ContentManager.CoreService.Client.SchemaPurporse" />
		/// </summary>
		/// <value>
		/// The <see cref="Schema" /> <see cref="T:Tridion.ContentManager.CoreService.Client.SchemaPurporse" />
		/// </value>
		public SchemaPurpose SchemaPurpose
		{
			get
			{
				return mSchemaData.Purpose.GetValueOrDefault(SchemaPurpose.UnknownByClient);
			}
			set
			{
				mSchemaData.Purpose = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Schema" /> root element name
		/// </summary>
		/// <value>
		/// The <see cref="Schema" /> root element name
		/// </value>
		public String RootElementName
		{
			get
			{
				return mSchemaData.RootElementName;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mSchemaData.RootElementName = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Schema" /> Xsd
		/// </summary>
		/// <value>
		/// The <see cref="Schema" /> Xsd
		/// </value>
		public String Xsd
		{
			get
			{
				return mSchemaData.Xsd;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mSchemaData.Xsd = value;
			}
		}
	}
}