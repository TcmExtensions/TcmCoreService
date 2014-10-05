#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Location
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
using System.Text;
using TcmCoreService.CommunicationManagement;
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="Location" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.LocationInfo" />
	/// </summary>
	public class Location : InfoBase
	{
		private LocationInfo mLocationInfo;
		private OrganizationalItem mOrganizationalItem = null;
		private Repository mContextRepository = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Location"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="locationInfo"><see cref="T:Tridion.ContentManager.CoreService.Client.LocationInfo" /></param>
		internal Location(Client client, LocationInfo locationInfo): base(client)
		{
			mLocationInfo = locationInfo;
		}

		/// <summary>
		/// Gets the <see cref="Location" /> path
		/// </summary>
		/// <value>
		/// <see cref="Location" /> path
		/// </value>
		public String Path
		{
			get
			{
				return mLocationInfo.Path;
			}
		}

		/// <summary>
		/// Gets the <see cref="Location" /> WebDav Url
		/// </summary>
		/// <value>
		/// <see cref="Location" /> WebDav Url
		/// </value>
		public String WebDavUrl
		{
			get
			{
				return mLocationInfo.WebDavUrl;
			}
		}

		/// <summary>
		/// Gets the <see cref="Location" /> context <see cref="T:TcmCoreService.ContentManagement.Repository" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.ContentManagement.Repository" />
		/// </value>
		public Repository ContextRepository
		{
			get
			{
				if (mContextRepository == null)
					mContextRepository = new Repository(Client, mLocationInfo.ContextRepository.IdRef);

				return mContextRepository;
			}
		}

		/// <summary>
		/// Retrieves the <see cref="T:TcmCoreService.ContentManagement.OrganizationalItem" /> for this <see cref="Location" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.ContentManagement.OrganizationalItem" /> for this <see cref="Location" />
		/// </value>
		public OrganizationalItem OrganizationalItem
		{
			get
			{
				if (mOrganizationalItem == null)
				{
					TcmUri uri = mLocationInfo.OrganizationalItem.IdRef;

					switch (uri.ItemType)
					{
						case (int)ItemType.Folder:
							mOrganizationalItem = new Folder(Client, uri);
							break;
						case (int)ItemType.VirtualFolder:
							mOrganizationalItem = new VirtualFolder(Client, uri);
							break;
						case (int)ItemType.Category:
							mOrganizationalItem = new Category(Client, uri);
							break;
						case (int)ItemType.StructureGroup:
							mOrganizationalItem = new StructureGroup(Client, uri);
							break;
						default:
							return null;
					}
				}

				return mOrganizationalItem;
			}
		}
	}
}
