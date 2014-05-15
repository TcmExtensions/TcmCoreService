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
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="systemWideObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.SystemWideObjectData" /></param>
		protected SystemWideObject(Session session, SystemWideObjectData systemWideObjectData): base(session, systemWideObjectData)
		{
			if (systemWideObjectData == null)
				throw new ArgumentNullException("systemWideObjectData");

			mSystemWideObjectData = systemWideObjectData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SystemWideObject"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal SystemWideObject(Session session, TcmUri uri): this(session, session.Read<SystemWideObjectData>(uri))
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
			Reload(Session.Read<SystemWideObjectData>(this.Id));
		}
	}
}
