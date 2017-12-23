using MessagingApiTemplate.Models.Requests.SendMessage.Template.Action;
using MessagingApiTemplate.Utils;
using System;

namespace MessagingApiTemplate.Services.Message.Factory{

	/// <summary>
	/// テンプレートのアクション作成用クラス
	/// </summary>
	public class TemplateActionFactoryService {

		/// <summary>
		/// アクションの配列
		/// </summary>
		public TemplateActionBase[] Actions;

		/// <summary>
		/// コンストラクタは隠す
		/// </summary>
		private TemplateActionFactoryService() { }

		/// <summary>
		/// アクション作成
		/// </summary>
		public static TemplateActionFactoryService CreateAction() {
			Trace.TraceInformation( "Start" );
			TemplateActionFactoryService templateActionFactoryService = new TemplateActionFactoryService();
			Trace.TraceInformation( "End" );
			return templateActionFactoryService;
		}

		/// <summary>
		/// 配列の数を追加する
		/// </summary>
		/// <returns>配列が既に5つたまっていた場合は何もしない</returns>
		private bool RegulateMessageArray() {

			Trace.TraceInformation( "Start" );

			if( this.Actions == null ) {
				Trace.TraceInformation( "Actions is Null" );
				this.Actions = new TemplateActionBase[ 1 ];
				Trace.TraceInformation( "End" );
				return true;
			}
			else {
				if( this.Actions.Length == 5 ) {
					Trace.TraceWarning( "Actions Length is Max" );
					Trace.TraceInformation( "End" );
					return false;
				}
				else {
					Trace.TraceInformation( "Actions Length is not Max" );
					TemplateActionBase[] action = this.Actions;
					Array.Resize(
						ref action ,
						this.Actions.Length + 1
					);
					this.Actions = action;
					Trace.TraceInformation( "End" );
					return true;
				}
			}

		}

		/// <summary>
		/// ポストバックアクション追加
		/// </summary>
		/// <param name="text">テキスト</param>
		/// <param name="templateActionFactoryService">アクション作成用クラス</param>
		public TemplateActionFactoryService AddConfirmTemplateMessage(
			string label ,
			string text ,
			string data
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Action Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			TemplateActionBase action = new TemplatePostbackAction() {
				label = label ,
				text = text ,
				data = data
			};
			this.Actions[ this.Actions.Length - 1 ] = action;

			Trace.TraceInformation( "End" );
				 
			return this;
		}

		/// <summary>
		/// メッセージアクション追加
		/// </summary>
		/// <param name="text">テキスト</param>
		/// <param name="templateActionFactoryService">アクション作成用クラス</param>
		public TemplateActionFactoryService AddMessageTemplateMessage(
			string label ,
			string text
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Action Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			TemplateActionBase action = new TemplateMessageAction() {
				label = label ,
				text = text
			};
			this.Actions[ this.Actions.Length - 1 ] = action;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// URIアクション追加
		/// </summary>
		/// <param name="text">テキスト</param>
		/// <param name="templateActionFactoryService">アクション作成用クラス</param>
		public TemplateActionFactoryService AddUriTemplateMessage(
			string label ,
			string uri
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Action Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			TemplateActionBase action = new TemplateUriAction() {
				label = label ,
				uri = uri
			};
			this.Actions[ this.Actions.Length - 1 ] = action;

			Trace.TraceInformation( "End" );

			return this;
		}
		
	}

}