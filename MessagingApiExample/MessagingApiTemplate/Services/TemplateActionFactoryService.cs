using MessagingApiTemplate.Models.Requests.SendMessage.Template.Action;
using System;
using System.Diagnostics;

namespace MessagingApiTemplate.Services {

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
			TemplateActionFactoryService templateActionFactoryService = new TemplateActionFactoryService();
			return templateActionFactoryService;
		}

		/// <summary>
		/// 配列の数を追加する
		/// </summary>
		/// <returns>配列が既に5つたまっていた場合は何もしない</returns>
		private bool RegulateMessageArray() {

			Trace.TraceInformation( "Start Regulate Actions Array" );

			if( this.Actions == null ) {
				Trace.TraceInformation( "Actions is Null" );
				this.Actions = new TemplateActionBase[ 1 ];
				return true;
			}
			else {
				if( this.Actions.Length == 5 ) {
					Trace.TraceWarning( "Actions Length is Max" );
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

			Trace.TraceInformation( "Start Add Postback Action" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Action Array is False" );
				return this;
			}

			TemplateActionBase action = new TemplatePostbackAction() {
				label = label ,
				text = text ,
				data = data
			};
			this.Actions[ this.Actions.Length - 1 ] = action;

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

			Trace.TraceInformation( "Start Add Message Action" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Action Array is False" );
				return this;
			}

			TemplateActionBase action = new TemplateMessageAction() {
				label = label ,
				text = text
			};
			this.Actions[ this.Actions.Length - 1 ] = action;

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

			Trace.TraceInformation( "Start Add Uri Action" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Action Array is False" );
				return this;
			}

			TemplateActionBase action = new TemplateUriAction() {
				label = label ,
				uri = uri
			};
			this.Actions[ this.Actions.Length - 1 ] = action;

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

			Trace.TraceInformation( "Start Add Uri Action" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Action Array is False" );
				return this;
			}

			TemplateActionBase action = new TemplateDatetimepickerAction() {
				label = label ,
				uri = uri
			};
			this.Actions[ this.Actions.Length - 1 ] = action;

			return this;
		}

	}

}