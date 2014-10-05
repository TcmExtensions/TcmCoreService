using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="SystemWideObject" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.SystemWideObjectData" />.
	/// </summary>
	public abstract class SystemWideObject : IdentifiableObject
	{
		private SystemWideObjectData mSystemWideObjectData;

		/// <summary>
		/// Initializes a new instance of the <see cref="SystemWideObject"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="systemWideObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.SystemWideObjectData" /></param>
		protected SystemWideObject(Client client, SystemWideObjectData systemWideObjectData): base(client, systemWideObjectData)
		{
			if (systemWideObjectData == null)
				throw new ArgumentNullException("systemWideObjectData");

			mSystemWideObjectData = systemWideObjectData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SystemWideObject"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal SystemWideObject(Client client, TcmUri uri): this(client, client.Read<SystemWideObjectData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="SystemWideObject" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.SystemWideObjectData" />
		/// </summary>
		/// <param name="systemWideObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.SystemWideObjectData" /></param>
		protected void Reload(SystemWideObjectData systemWideObjectData)
		{
			if (systemWideObjectData == null)
				throw new ArgumentNullException("systemWideObjectData");

			mSystemWideObjectData = systemWideObjectData;
			base.Reload(systemWideObjectData);
		}

		/// <summary>
		/// Reload the <see cref="SystemWideObject" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<SystemWideObjectData>(this.Id));
		}
	}
}
