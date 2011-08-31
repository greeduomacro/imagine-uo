/* Support for Multiple Command Prefixes
 * Copyright (c) 2003 by Kevin L'Huillier
 * All rights reserved.
 * -
 * The RunUO default of `[' is very difficult for some international users
 * (a very poor choice!), but some utilities require the server use it.
 * `.' is preferred, but the 2D client will translate it to `=' (which some
 * users actually prefer) for those with the GM Body (0x3db or 987)
 * 
 * Edited for RunUO 2.0 by Florian Schaetz - Kleanthes@eurebia.net
 */

#region License (The MIT License)
/* Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * The Software is provided "as is", without warranty of any kind, express or
 * implied.
 */
#endregion

namespace Server.Commands
{
	public class MultipleCommandPrefixes
	{
		static char[] prefixes = new char[] {'.', '=', '['};

		public static void Initialize()
		{
			EventSink.Speech += new SpeechEventHandler(EventSink_Speech);
		}

		static void EventSink_Speech(SpeechEventArgs e)
		{
			string cmd = ((e.Speech.Length > 1) ? e.Speech.Substring(1) : "");

			for (int i = 0; i < prefixes.Length && !e.Blocked; i++) {
				if (e.Speech[0] == prefixes[i]) {
					Server.Commands.CommandSystem.Handle(e.Mobile, Server.Commands.CommandSystem.Prefix + cmd);
					e.Blocked = true;
				}
			}
		}
	}
}
