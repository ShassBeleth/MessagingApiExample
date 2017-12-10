using System;
using System.Diagnostics;
using MessagingApiExample.Models.Request.ReplyMessage.Message;

namespace MessagingApiExample.Services.MessageFactory {

	/// <summary>
	/// メッセージ作成Service
	/// </summary>
	public class MessageFactoryService {

		/// <summary>
		/// メッセージ
		/// </summary>
		public MessageBase[] Messages { private set; get; }

		/// <summary>
		/// コンストラクタは隠す
		/// </summary>
		private MessageFactoryService() { }

		/// <summary>
		/// メッセージ作成
		/// </summary>
		public static MessageFactoryService CreateMessage() {
			MessageFactoryService messageFactoryServiec = new MessageFactoryService();
			return messageFactoryServiec;
		}

		/// <summary>
		/// 配列の数を追加する
		/// </summary>
		/// <returns>配列が既に5つたまっていた場合は何もしない</returns>
		private bool RegulateMessageArray() {

			Trace.TraceInformation( "Start Regulate Message Array" );

			if( this.Messages == null ) {
				Trace.TraceInformation( "Messages is Null" );
				this.Messages = new MessageBase[ 1 ];
				return true;
			}
			else {
				if( this.Messages.Length == 5 ) {
					Trace.TraceWarning( "Messages Length is Max" );
					return false;
				}
				else {
					Trace.TraceInformation( "Messages Length is not Max" );
					MessageBase[] messages = this.Messages;
					Array.Resize(
						ref messages ,
						this.Messages.Length + 1
					);
					this.Messages = messages;
					return true;
				}

			}

		}

		/// <summary>
		/// テキストメッセージ追加
		/// </summary>
		/// <param name="text">テキスト</param>
		public MessageFactoryService AddTextMessage( string text ) {

			Trace.TraceInformation( "Start Add Text Message" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				return this;
			}

			TextMessage textMessage = new TextMessage() {
				text = text
			};
			this.Messages[ this.Messages.Length - 1 ] = textMessage;

			return this;

		}

		/// <summary>
		/// スタンプメッセージ追加
		/// </summary>
		/// <param name="packageId">パッケージID</param>
		/// <param name="stickerId">スタンプID</param>
		public MessageFactoryService AddStickerMessage( string packageId , string stickerId ) {

			Trace.TraceInformation( "Start Add Sticker Message" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				return this;
			}

			StickerMessage stickerMessage = new StickerMessage() {
				packageId = packageId ,
				stickerId = stickerId
			};
			this.Messages[ this.Messages.Length - 1 ] = stickerMessage;

			return this;

		}

		/// <summary>
		/// 画像メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">画像URL</param>
		/// <param name="previewImageUrl">プレビュー画像URL</param>
		public MessageFactoryService AddImageMessage( string originalContentUrl , string previewImageUrl ) {

			Trace.TraceInformation( "Start Add Image Message" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				return this;
			}

			ImageMessage imageMessage = new ImageMessage() {
				originalContentUrl = originalContentUrl ,
				previewImageUrl = previewImageUrl
			};
			this.Messages[ this.Messages.Length - 1 ] = imageMessage;

			return this;

		}

		/// <summary>
		/// 動画メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">動画URL</param>
		/// <param name="previewImageUrl">プレビュー画像URL</param>
		public MessageFactoryService AddVideoMessage( string originalContentUrl , string previewImageUrl ) {

			Trace.TraceInformation( "Start Add Video Message" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				return this;
			}

			VideoMessage videoMessage = new VideoMessage() {
				originalContentUrl = originalContentUrl ,
				previewImageUrl = previewImageUrl
			};
			this.Messages[ this.Messages.Length - 1 ] = videoMessage;

			return this;

		}

		/// <summary>
		/// 音声メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">音声URL</param>
		/// <param name="duration">音声ファイルの長さ</param>
		public MessageFactoryService AddAudioMessage( string originalContentUrl , int duration ) {

			Trace.TraceInformation( "Start Add Audio Message" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				return this;
			}

			AudioMessage audioMessage = new AudioMessage() {
				originalContentUrl = originalContentUrl ,
				duration = duration
			};
			this.Messages[ this.Messages.Length - 1 ] = audioMessage;

			return this;

		}

		/// <summary>
		/// 位置情報メッセージ追加
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="address">住所</param>
		/// <param name="latitude">緯度</param>
		/// <param name="longitude">経度</param>
		public MessageFactoryService AddLocationMessage(
			string title ,
			string address ,
			decimal latitude ,
			decimal longitude
		) {

			Trace.TraceInformation( "Start Add Location Message" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				return this;
			}

			LocationMessage locationMessage = new LocationMessage() {
				title = title ,
				address = address ,
				latitude = latitude ,
				longitude = longitude
			};
			this.Messages[ this.Messages.Length - 1 ] = locationMessage;

			return this;

		}


	}

}