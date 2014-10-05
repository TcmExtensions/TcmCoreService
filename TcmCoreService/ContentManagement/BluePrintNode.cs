#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Blue Print Node
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
using TcmCoreService.CommunicationManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="BluePrintNode" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.BluePrintNodeData" />.
	/// </summary>
	public class BluePrintNode : Publication
	{
		private BluePrintNodeData mBluePrintNodeData;

		private RepositoryLocalObject mItem = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="BluePrintNode"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="bluePrintNodeData"><see cref="T:Tridion.ContentManager.CoreService.Client.BluePrintNodeData" /></param>
		protected BluePrintNode(Session session, BluePrintNodeData bluePrintNodeData): base(session, bluePrintNodeData)
		{
			if (bluePrintNodeData == null)
				throw new ArgumentNullException("bluePrintNodeData");

			mBluePrintNodeData = bluePrintNodeData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BluePrintNode"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal BluePrintNode(Session session, TcmUri uri): this(session, session.Read<BluePrintNodeData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Publication" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.BluePrintNodeData" />
		/// </summary>
		/// <param name="bluePrintNodeData"><see cref="T:Tridion.ContentManager.CoreService.Client.BluePrintNodeData" /></param>
		protected void Reload(BluePrintNodeData bluePrintNodeData)
		{
			if (bluePrintNodeData == null)
				throw new ArgumentNullException("bluePrintNodeData");

			mBluePrintNodeData = bluePrintNodeData;
			base.Reload(bluePrintNodeData);

			mItem = null;
		}

		/// <summary>
		/// Reload the <see cref="Publication" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<BluePrintNodeData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets the <see cref="RepositoryLocalObject" /> item of this <see cref="BluePrintNode" />
		/// </summary>
		/// <value>
		/// The <see cref="RepositoryLocalObject" /> item of this <see cref="BluePrintNode" />
		/// </value>
		public RepositoryLocalObject Item
		{
			get
			{
				TcmUri uri = mBluePrintNodeData.Item.Id;

				if (mItem == null)
				{
					switch (uri.ItemType)
					{
						case (int)ItemType.Folder:
							mItem = new Folder(Client, uri);
							break;
						case (int)ItemType.VirtualFolder:
							mItem = new VirtualFolder(Client, uri);
							break;
						case (int)ItemType.Category:
							mItem = new Category(Client, uri);
							break;
						case (int)ItemType.StructureGroup:
							mItem = new StructureGroup(Client, uri);
							break;
						case (int)ItemType.Component:
							mItem = new Component(Client, uri);
							break;
						case (int)ItemType.ComponentTemplate:
							mItem = new ComponentTemplate(Client, uri);
							break;
						case (int)ItemType.Schema:
							mItem = new Schema(Client, uri);
							break;
						case (int)ItemType.Keyword:
							mItem = new Keyword(Client, uri);
							break;
						case (int)ItemType.PageTemplate:
							mItem = new PageTemplate(Client, uri);
							break;
						case (int)ItemType.TemplateBuildingBlock:
							mItem = new TemplateBuildingBlock(Client, uri);
							break;
					}
				}

				return mItem;				
			}
			set
			{
				mItem = value;

				if (value == null)
					mBluePrintNodeData.Item.Id = TcmUri.NullUri;
				else
					mBluePrintNodeData.Item.Id = value.Id;
			}
		}
	}
}
