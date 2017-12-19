using System;
using MessagingApiTemplate.Models.Requests.SendMessage.ImageMap;
using MessagingApiTemplate.Utils;

namespace MessagingApiTemplate.Services.Message.Factory {

	/// <summary>
	/// イメージマップアクション作成Service
	/// </summary>
	public class ImageMapActionFactoryService {

		/// <summary>
		/// アクション
		/// </summary>
		internal ImageMapActionBase[] Actions { private set; get; }

		/// <summary>
		/// コンストラクタは隠す
		/// </summary>
		private ImageMapActionFactoryService() { }

		/// <summary>
		/// アクション作成
		/// </summary>
		public static ImageMapActionFactoryService CreateAction() {
			ImageMapActionFactoryService imageMapActionFactoryService = new ImageMapActionFactoryService();
			return imageMapActionFactoryService;
		}

		/// <summary>
		/// 配列の数を追加する
		/// </summary>
		/// <returns>配列が既に50つたまっていた場合は何もしない</returns>
		private bool RegulateImageMapActionArray() {

			Trace.TraceInformation( "Start" );

			if( this.Actions == null ) {
				Trace.TraceInformation( "Image Map Action is Null" );
				this.Actions = new ImageMapActionBase[ 1 ];
				Trace.TraceInformation( "End" );
				return true;
			}
			else {
				if( this.Actions.Length == 50 ) {
					Trace.TraceWarning( "Image Map Action Length is Max" );
					Trace.TraceInformation( "End" );
					return false;
				}
				else {
					Trace.TraceInformation( "Image Map Action Length is not Max" );
					ImageMapActionBase[] actions = this.Actions;
					Array.Resize(
						ref actions ,
						this.Actions.Length + 1
					);
					this.Actions = actions;
					Trace.TraceInformation( "End" );
					return true;
				}
			}

		}

		/// <summary>
		/// イメージマップURIアクションを追加する
		/// </summary>
		/// <param name="linkUri">リダイレクト先URI</param>
		/// <param name="x">領域の左上角からの横方向の相対位置</param>
		/// <param name="y">領域の左上角からの縦方向の相対位置</param>
		/// <param name="width">タップ領域の幅</param>
		/// <param name="height">タップ領域の高さ</param>
		public ImageMapActionFactoryService AddUrlAction(
			string linkUri ,
			int x ,
			int y ,
			int width ,
			int height
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateImageMapActionArray() ) {
				Trace.TraceWarning( "Regulate Image Map Action Array is False" );
				return this;
			}

			ImageMapUriAction imageMapUriAction = new ImageMapUriAction() {
				linkUri = linkUri ,
				area = new ImageMapArea() {
					x = x ,
					y = y ,
					width = width ,
					height = height
				}
			};

			this.Actions[ this.Actions.Length - 1 ] = imageMapUriAction;

			Trace.TraceInformation( "Start" );

			return this;

		}

		/// <summary>
		/// イメージマップメッセージアクションを追加する
		/// </summary>
		/// <param name="text">リダイレクト先URI</param>
		/// <param name="x">領域の左上角からの横方向の相対位置</param>
		/// <param name="y">領域の左上角からの縦方向の相対位置</param>
		/// <param name="width">タップ領域の幅</param>
		/// <param name="height">タップ領域の高さ</param>
		public ImageMapActionFactoryService AddMessageAction(
			string text ,
			int x ,
			int y ,
			int width ,
			int height
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateImageMapActionArray() ) {
				Trace.TraceWarning( "Regulate Image Map Action Array is False" );
				return this;
			}

			ImageMapMessageAction imageMapMessageAction = new ImageMapMessageAction() {
				text = text ,
				area = new ImageMapArea() {
					x = x ,
					y = y ,
					width = width ,
					height = height
				}
			};

			this.Actions[ this.Actions.Length - 1 ] = imageMapMessageAction;

			Trace.TraceInformation( "End" );

			return this;

		}

	}

}