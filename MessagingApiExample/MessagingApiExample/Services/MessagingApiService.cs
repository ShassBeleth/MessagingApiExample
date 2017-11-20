using System.Diagnostics;
using MessagingApiExample.Models.Request.Webhook.Body;
using MessagingApiExample.Models.Webhook.Body.Event;
using Newtonsoft.Json.Linq;

namespace MessagingApiExample.Services {

	/// <summary>
	/// Messaging API用サービス
	/// </summary>
	public class MessagingApiService {

		/// <summary>
		/// JTokenからWebhookRequestデータモデルに変換する
		/// </summary>
		/// <param name="token">リクエストより送られたJToken</param>
		/// <returns>WebhookRequest</returns>
		public WebhookRequest ConvertJTokenToWebhookRequest( JToken token ) {

			Trace.TraceInformation( "Token is : " + token );

			WebhookRequest webhookRequest = new WebhookRequest();

			// Event数を取得
			JArray events = (JArray)token[ "events" ];
			webhookRequest.events = new EventBase[ events.Count ];
			Trace.TraceInformation( "Event Count is : " + events.Count );

			// TypeからEventを割り出す
			for( int i = 0 ; i < webhookRequest.events.Length ; i++ ) {

				Trace.TraceInformation( "Event Index is : " + i );

				string type = events[ i ][ "type" ].Value<string>();
				Trace.TraceInformation( "Event Type is : " + type );

				switch( type ) {

					case "beacon":
						webhookRequest.events[ i ] = new BeaconEvent();
						break;

					case "follow":
						webhookRequest.events[ i ] = new FollowEvent();
						break;

					case "join":
						webhookRequest.events[ i ] = new JoinEvent();
						break;

					case "leave":
						webhookRequest.events[ i ] = new LeaveEvent();
						break;

					case "message":
						webhookRequest.events[ i ] = new MessageEvent();
						break;

					case "postback":
						webhookRequest.events[ i ] = new PostbackEvent();
						break;

					case "unfollow":
						webhookRequest.events[ i ] = new UnfollowEvent();
						break;

					default:
						return null;

				}

				JValue source = (JValue)events[ i ][ "source" ];


			}
			
			return null;
		}

	}

}