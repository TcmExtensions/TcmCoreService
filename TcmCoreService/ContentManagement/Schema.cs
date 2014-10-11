#region Header
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
        private ProcessDefinition mBundleProcess = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Schema"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="schemaData"><see cref="T:Tridion.ContentManager.CoreService.Client.SchemaData" /></param>
		protected Schema(Client client, SchemaData schemaData): base(client, schemaData)
		{
			if (schemaData == null)
				throw new ArgumentNullException("schemaData");

			mSchemaData = schemaData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Schema"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Schema(Client client, TcmUri uri): this(client, client.Read<SchemaData>(uri))
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
            mBundleProcess = null;
		}

		/// <summary>
		/// Reload the <see cref="Schema" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<SchemaData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="Schema" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<SchemaData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="Schema" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<SchemaData>(this.Id));			
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
					mAllowedMultimediaTypes = mSchemaData.AllowedMultimediaTypes.Select(multimediaType => new MultimediaType(Client, multimediaType.IdRef));

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
        /// Gets or sets the <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.ContentManagement.MultimediaType" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Schema" />
        /// </summary>
        /// <value>
        /// <see cref="Schema" /> allowed <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.ContentManagement.MultimediaType" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Schema" />
        /// </value>
        public IEnumerable<TcmUri> AllowedMultimediaTypesUri
        {
            get
            {
                return mSchemaData.AllowedMultimediaTypes.Select(multimediaType => new TcmUri(multimediaType.IdRef));
            }
            set
            {
                mAllowedMultimediaTypes = null;

                if (value != null)
                {
                    mSchemaData.AllowedMultimediaTypes = value.Select(multimediaType => new LinkToMultimediaTypeData()
                    {
                        IdRef = multimediaType
                    }).ToArray();
                }
                else
                    mSchemaData.AllowedMultimediaTypes = null;
            }
        }

        /// <summary>
        /// Gets or sets the bundle <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Schema" />
        /// </summary>
        /// <value>
        /// Bundle <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Schema" />
        /// </value>
        public ProcessDefinition BundleProcess
        {
            get
            {
                if (mBundleProcess == null && mSchemaData.BundleProcess != null)
                    mBundleProcess = new TridionProcessDefinition(Client, mSchemaData.BundleProcess.IdRef);

                return mBundleProcess;
            }
            set
            {
                mBundleProcess = value;

                if (value != null)
                    mSchemaData.BundleProcess.IdRef = value.Id;
                else
                    mSchemaData.BundleProcess.IdRef = TcmUri.NullUri;
            }
        }

        /// <summary>
        /// Gets or sets the bundle <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Schema" />
        /// </summary>
        /// <value>
        /// Bundle <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Schema" />
        /// </value>
        public TcmUri BundleProcessUri
        {
            get
            {
                return mSchemaData.BundleProcess.IdRef;
            }
            set
            {
                mBundleProcess = null;

                if (value == null)
                    mSchemaData.BundleProcess.IdRef = TcmUri.NullUri;
                else
                    mSchemaData.BundleProcess.IdRef = value;
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
					mComponentProcess = new TridionProcessDefinition(Client, mSchemaData.ComponentProcess.IdRef);

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
        /// Gets or sets the component <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Schema" />
        /// </summary>
        /// <value>
        /// Component <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Schema" />
        /// </value>
        public TcmUri ComponentProcessUri
        {
            get
            {
                return mSchemaData.ComponentProcess.IdRef;
            }
            set
            {
                mComponentProcess = null;

                if (value == null)
                    mSchemaData.ComponentProcess.IdRef = TcmUri.NullUri;
                else
                    mSchemaData.ComponentProcess.IdRef = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Schema" /> bundle should be deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="Schema" /> bundle should be deleted; otherwise, <c>false</c>.
        /// </value>
        public Boolean DeleteBundleOnProcessFinished
        {
            get
            {
                return mSchemaData.DeleteBundleOnProcessFinished.GetValueOrDefault(false);
            }
            set
            {
                mSchemaData.DeleteBundleOnProcessFinished = value;
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
