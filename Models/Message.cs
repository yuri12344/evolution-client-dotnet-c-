using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Evolution.ClientAPI.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaType
    {
        image,
        video,
        document
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusType
    {
        text,
        image,
        video,
        audio
    }

    public enum FontType
    {
        Serif = 1,
        NoricanRegular = 2,
        BryndanWrite = 3,
        BebasneueRegular = 4,
        OswaldHeavy = 5
    }

    public class QuotedMessage
    {
        public Dictionary<string, object> Key { get; set; }
        public Dictionary<string, object> Message { get; set; }
    }

    public class TextMessage
    {
        public string Number { get; set; }
        public string Text { get; set; }
        public int? Delay { get; set; }
        public QuotedMessage Quoted { get; set; }
        public bool? LinkPreview { get; set; }
        public bool? MentionsEveryOne { get; set; }
        public List<string> Mentioned { get; set; }

        public TextMessage(string number, string text, int? delay = null, QuotedMessage quoted = null,
            bool? linkPreview = null, bool? mentionsEveryOne = null, List<string> mentioned = null)
        {
            Number = number;
            Text = text;
            Delay = delay;
            Quoted = quoted;
            LinkPreview = linkPreview;
            MentionsEveryOne = mentionsEveryOne;
            Mentioned = mentioned;
        }
    }

    public class MediaMessage
    {
        public string Number { get; set; }
        public string Mediatype { get; set; }
        public string Mimetype { get; set; }
        public string Caption { get; set; }
        public string Media { get; set; }
        public string FileName { get; set; }
        public int? Delay { get; set; }
        public QuotedMessage Quoted { get; set; }
        public bool? MentionsEveryOne { get; set; }
        public List<string> Mentioned { get; set; }

        public MediaMessage(string number, string mediatype, string mimetype, string caption, string media,
            string fileName, int? delay = null, QuotedMessage quoted = null, bool? mentionsEveryOne = null,
            List<string> mentioned = null)
        {
            Number = number;
            Mediatype = mediatype;
            Mimetype = mimetype;
            Caption = caption;
            Media = media;
            FileName = fileName;
            Delay = delay;
            Quoted = quoted;
            MentionsEveryOne = mentionsEveryOne;
            Mentioned = mentioned;
        }
    }

    public class StatusMessage
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public string Caption { get; set; }
        public string BackgroundColor { get; set; }
        public int? Font { get; set; }
        public bool AllContacts { get; set; }
        public List<string> StatusJidList { get; set; }

        public StatusMessage(StatusType type, string content, string caption = null, string backgroundColor = null,
            FontType? font = null, bool allContacts = false, List<string> statusJidList = null)
        {
            Type = type.ToString();
            Content = content;
            Caption = caption;
            BackgroundColor = backgroundColor;
            Font = font.HasValue ? (int)font.Value : (int?)null;
            AllContacts = allContacts;
            StatusJidList = statusJidList;
        }
    }

    public class LocationMessage
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int? Delay { get; set; }
        public QuotedMessage Quoted { get; set; }

        public LocationMessage(string number, string name, string address, float latitude, float longitude,
            int? delay = null, QuotedMessage quoted = null)
        {
            Number = number;
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            Delay = delay;
            Quoted = quoted;
        }
    }

    public class Contact
    {
        public string FullName { get; set; }
        public string Wuid { get; set; }
        public string PhoneNumber { get; set; }
        public string Organization { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }

        public Contact(string fullName, string wuid, string phoneNumber, string organization = null,
            string email = null, string url = null)
        {
            FullName = fullName;
            Wuid = wuid;
            PhoneNumber = phoneNumber;
            Organization = organization;
            Email = email;
            Url = url;
        }
    }

    public class ContactMessage
    {
        public string Number { get; set; }
        public List<Contact> Contact { get; set; }

        public ContactMessage(string number, List<Contact> contact)
        {
            Number = number;
            Contact = contact;
        }
    }

    public class ReactionMessage
    {
        public Dictionary<string, object> Key { get; set; }
        public string Reaction { get; set; }

        public ReactionMessage(Dictionary<string, object> key, string reaction)
        {
            Key = key;
            Reaction = reaction;
        }
    }

    public class PollMessage
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int SelectableCount { get; set; }
        public List<string> Values { get; set; }
        public int? Delay { get; set; }
        public QuotedMessage Quoted { get; set; }

        public PollMessage(string number, string name, int selectableCount, List<string> values,
            int? delay = null, QuotedMessage quoted = null)
        {
            Number = number;
            Name = name;
            SelectableCount = selectableCount;
            Values = values;
            Delay = delay;
            Quoted = quoted;
        }
    }

    public class ListRow
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RowId { get; set; }

        public ListRow(string title, string description, string rowId)
        {
            Title = title;
            Description = description;
            RowId = rowId;
        }
    }

    public class ListSection
    {
        public string Title { get; set; }
        public List<ListRow> Rows { get; set; }

        public ListSection(string title, List<ListRow> rows)
        {
            Title = title;
            Rows = rows;
        }
    }

    public class ListMessage
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string FooterText { get; set; }
        public List<ListSection> Sections { get; set; }
        public int? Delay { get; set; }
        public QuotedMessage Quoted { get; set; }

        public ListMessage(string number, string title, string description, string buttonText, string footerText,
            List<ListSection> sections, int? delay = null, QuotedMessage quoted = null)
        {
            Number = number;
            Title = title;
            Description = description;
            ButtonText = buttonText;
            FooterText = footerText;
            Sections = sections;
            Delay = delay;
            Quoted = quoted;
        }
    }

    public class Button
    {
        public string Type { get; set; }
        public string DisplayText { get; set; }
        public string Id { get; set; }
        public string CopyCode { get; set; }
        public string Url { get; set; }
        public string PhoneNumber { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string KeyType { get; set; }
        public string Key { get; set; }

        public Button(string type, string displayText, string id = null, string copyCode = null,
            string url = null, string phoneNumber = null, string currency = null, string name = null,
            string keyType = null, string key = null)
        {
            Type = type;
            DisplayText = displayText;
            Id = id;
            CopyCode = copyCode;
            Url = url;
            PhoneNumber = phoneNumber;
            Currency = currency;
            Name = name;
            KeyType = keyType;
            Key = key;
        }
    }

    public class ButtonMessage
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Footer { get; set; }
        public List<Button> Buttons { get; set; }
        public int? Delay { get; set; }
        public QuotedMessage Quoted { get; set; }

        public ButtonMessage(string number, string title, string description, string footer,
            List<Button> buttons, int? delay = null, QuotedMessage quoted = null)
        {
            Number = number;
            Title = title;
            Description = description;
            Footer = footer;
            Buttons = buttons;
            Delay = delay;
            Quoted = quoted;
        }
    }
}
