using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace CompanyX.PaymentGateway.WebApi.Formatters
{
	public class As400XmlMediaTypeFormatter : XmlMediaTypeFormatter
	{
		private Encoding as400Encoding = Encoding.GetEncoding("iso-8859-1");

		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (string.IsNullOrEmpty(content.Headers.ContentType.CharSet)) //as400 team can't set charset (or don't want to) so assume ISO-8859-1
			{
				/* no need to dispose - read at the end of file */
				var resultStream = ConvertStream(readStream, as400Encoding, Encoding.UTF8);
				return base.ReadFromStreamAsync(type, resultStream, content, formatterLogger);
			}

			return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);   //if charset set, process normal
		}

		private Stream ConvertStream(Stream input, Encoding sourceEnc, Encoding destEnc)
		{
			//Encoding conversion must be applied to the whole byte array, chunked would not work due to bytes being split
			byte[] wholeDataBody = ToByteArray(input);

			/* no need to dispose - read at the end of file */
			var output = new MemoryStream(Encoding.Convert(sourceEnc, destEnc, wholeDataBody));
			return output;
		}

		private byte[] ToByteArray(Stream sourceStream)
		{
			using (var memoryStream = new MemoryStream())
			{
				sourceStream.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}

		/* In general, it is the responsibility of the consumer to properly dispose of a Disposable object.
		 * As such, if you pass off a Stream to another object, 
		 * you shouldn't Dispose it - that would be the responsibility of the consumer. */
	}
}