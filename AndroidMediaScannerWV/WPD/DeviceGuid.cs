using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMediaScannerWV
{
    static class DeviceGuid
    {
        public static Dictionary<Guid, string> GuidList { get; private set; }

        static DeviceGuid()
        {
            GetDeviceGuidList();
        }

        private static void GetDeviceGuidList()
        {
            List<FieldInfo> fieldList = typeof(DeviceGuid).GetFields(BindingFlags.NonPublic | BindingFlags.Static).ToList();

            GuidList = new Dictionary<Guid, string>();

            foreach (FieldInfo field in fieldList)
            {
                var value = field.GetValue(field);

                if (value is Guid)
                {
                    GuidList.Add((Guid)value, field.Name);
                }
            }
        }

        internal static Guid GUID_DEVINTERFACE_WPD = new Guid("6ac27878-a6fa-4155-ba85-f98f491d4f33");
        internal static Guid GUID_DEVINTERFACE_WPD_PRIVATE = new Guid("ba0c718f-4ded-49b7-bdd3-fabe28661211");
        internal static Guid GUID_DEVINTERFACE_WPD_SERVICE = new Guid("9ef44f80-3d64-4246-a6aa-206f328d1edc");
        internal static Guid WPD_API_OPTIONS_V1 = new Guid("10e54a3e-052d-4777-a13c-de7614be2bc4");
        internal static Guid WPD_APPOINTMENT_OBJECT_PROPERTIES_V1 = new Guid("f99efd03-431d-40d8-a1c9-4e220d9c88d3");
        internal static Guid WPD_CATEGORY_CAPABILITIES = new Guid("0cabec78-6b74-41c6-9216-2639d1fce356");
        internal static Guid WPD_CATEGORY_COMMON = new Guid("f0422a9c-5dc8-4440-b5bd-5df28835658a");
        internal static Guid WPD_CATEGORY_DEVICE_HINTS = new Guid("0d5fb92b-cb46-4c4f-8343-0bc3d3f17c84");
        internal static Guid WPD_CATEGORY_MEDIA_CAPTURE = new Guid("59b433ba-fe44-4d8d-808c-6bcb9b0f15e8");
        internal static Guid WPD_CATEGORY_NETWORK_CONFIGURATION = new Guid("78f9c6fc-79b8-473c-9060-6bd23dd072c4");
        internal static Guid WPD_CATEGORY_NULL = new Guid("00000000-0000-0000-0000-000000000000");
        internal static Guid WPD_CATEGORY_OBJECT_ENUMERATION = new Guid("b7474e91-e7f8-4ad9-b400-ad1a4b58eeec");
        internal static Guid WPD_CATEGORY_OBJECT_MANAGEMENT = new Guid("ef1e43dd-a9ed-4341-8bcc-186192aea089");
        internal static Guid WPD_CATEGORY_OBJECT_PROPERTIES = new Guid("9e5582e4-0814-44e6-981a-b2998d583804");
        internal static Guid WPD_CATEGORY_OBJECT_PROPERTIES_BULK = new Guid("11c824dd-04cd-4e4e-8c7b-f6efb794d84e");
        internal static Guid WPD_CATEGORY_OBJECT_RESOURCES = new Guid("b3a2b22d-a595-4108-be0a-fc3c965f3d4a");
        internal static Guid WPD_CATEGORY_SERVICE_CAPABILITIES = new Guid("24457e74-2e9f-44f9-8c57-1d1bcb170b89");
        internal static Guid WPD_CATEGORY_SERVICE_COMMON = new Guid("322f071d-36ef-477f-b4b5-6f52d734baee");
        internal static Guid WPD_CATEGORY_SERVICE_METHODS = new Guid("2d521ca8-c1b0-4268-a342-cf19321569bc");
        internal static Guid WPD_CATEGORY_SMS = new Guid("afc25d66-fe0d-4114-9097-970c93e920d1");
        internal static Guid WPD_CATEGORY_STILL_IMAGE_CAPTURE = new Guid("4fcd6982-22a2-4b05-a48b-62d38bf27b32");
        internal static Guid WPD_CATEGORY_STORAGE = new Guid("d8f907a6-34cc-45fa-97fb-d007fa47ec94");
        internal static Guid WPD_CLASS_EXTENSION_OPTIONS_V1 = new Guid("6309ffef-a87c-4ca7-8434-797576e40a96");
        internal static Guid WPD_CLASS_EXTENSION_OPTIONS_V2 = new Guid("3e3595da-4d71-49fe-a0b4-d4406c3ae93f");
        internal static Guid WPD_CLASS_EXTENSION_V1 = new Guid("33fb0d11-64a3-4fac-b4c7-3dfeaa99b051");
        internal static Guid WPD_CLASS_EXTENSION_V2 = new Guid("7f0779b5-fa2b-4766-9cb2-f73ba30b6758");
        internal static Guid WPD_CLIENT_INFORMATION_PROPERTIES_V1 = new Guid("204d9f0c-2292-4080-9f42-40664e70f859");
        internal static Guid WPD_COMMON_INFORMATION_OBJECT_PROPERTIES_V1 = new Guid("b28ae94b-05a4-4e8e-be01-72cc7e099d8f");
        internal static Guid WPD_PROPERTY_COMMON_COMMAND_CATEGORY = new Guid("4d545058-1a2e-4106-a357-771e0819fc56");
        internal static Guid WPD_CONTACT_OBJECT_PROPERTIES_V1 = new Guid("fbd4fdab-987d-4777-b3f9-726185a9312b");
        internal static Guid WPD_CONTENT_TYPE_ALL = new Guid("80e170d2-1055-4a3e-b952-82cc4f8a8689");
        internal static Guid WPD_CONTENT_TYPE_APPOINTMENT = new Guid("0fed060e-8793-4b1e-90c9-48ac389ac631");
        internal static Guid WPD_CONTENT_TYPE_AUDIO = new Guid("4ad2c85e-5e2d-45e5-8864-4f229e3c6cf0");
        internal static Guid WPD_CONTENT_TYPE_AUDIO_ALBUM = new Guid("aa18737e-5009-48fa-ae21-85f24383b4e6");
        internal static Guid WPD_CONTENT_TYPE_CALENDAR = new Guid("a1fd5967-6023-49a0-9df1-f8060be751b0");
        internal static Guid WPD_CONTENT_TYPE_CERTIFICATE = new Guid("dc3876e8-a948-4060-9050-cbd77e8a3d87");
        internal static Guid WPD_CONTENT_TYPE_CONTACT = new Guid("eaba8313-4525-4707-9f0e-87c6808e9435");
        internal static Guid WPD_CONTENT_TYPE_CONTACT_GROUP = new Guid("346b8932-4c36-40d8-9415-1828291f9de9");
        internal static Guid WPD_CONTENT_TYPE_DOCUMENT = new Guid("680adf52-950a-4041-9b41-65e393648155");
        internal static Guid WPD_CONTENT_TYPE_EMAIL = new Guid("8038044a-7e51-4f8f-883d-1d0623d14533");
        internal static Guid WPD_CONTENT_TYPE_FOLDER = new Guid("27E2E392-A111-48E0-AB0C-E17705A05F85");
        internal static Guid WPD_CONTENT_TYPE_FUNCTIONAL_OBJECT = new Guid("99ed0160-17ff-4c44-9d98-1d7a6f941921");
        internal static Guid WPD_CONTENT_TYPE_GENERIC_FILE = new Guid("0085e0a6-8d34-45d7-bc5c-447e59c73d48");
        internal static Guid WPD_CONTENT_TYPE_GENERIC_MESSAGE = new Guid("e80eaaf8-b2db-4133-b67e-1bef4b4a6e5f");
        internal static Guid WPD_CONTENT_TYPE_IMAGE = new Guid("ef2107d5-a52a-4243-a26b-62d4176d7603");
        internal static Guid WPD_CONTENT_TYPE_IMAGE_ALBUM = new Guid("75793148-15f5-4a30-a813-54ed8a37e226");
        internal static Guid WPD_CONTENT_TYPE_MEDIA_CAST = new Guid("5e88b3cc-3e65-4e62-bfff-229495253ab0");
        internal static Guid WPD_CONTENT_TYPE_MEMO = new Guid("9cd20ecf-3b50-414f-a641-e473ffe45751");
        internal static Guid WPD_CONTENT_TYPE_MIXED_CONTENT_ALBUM = new Guid("00f0c3ac-a593-49ac-9219-24abca5a2563");
        internal static Guid WPD_CONTENT_TYPE_NETWORK_ASSOCIATION = new Guid("031da7ee-18c8-4205-847e-89a11261d0f3");
        internal static Guid WPD_CONTENT_TYPE_PLAYLIST = new Guid("1a33f7e4-af13-48f5-994e-77369dfe04a3");
        internal static Guid WPD_CONTENT_TYPE_PROGRAM = new Guid("d269f96a-247c-4bff-98fb-97f3c49220e6");
        internal static Guid WPD_CONTENT_TYPE_SECTION = new Guid("821089f5-1d91-4dc9-be3c-bbb1b35b18ce");
        internal static Guid WPD_CONTENT_TYPE_TASK = new Guid("63252f2c-887f-4cb6-b1ac-d29855dcef6c");
        internal static Guid WPD_CONTENT_TYPE_TELEVISION = new Guid("60a169cf-f2ae-4e21-9375-9677f11c1c6e");
        internal static Guid WPD_CONTENT_TYPE_UNSPECIFIED = new Guid("28d8d31e-249c-454e-aabc-34883168e634");
        internal static Guid WPD_CONTENT_TYPE_VIDEO = new Guid("9261b03c-3d78-4519-85e3-02c5e1f50bb9");
        internal static Guid WPD_CONTENT_TYPE_VIDEO_ALBUM = new Guid("012b0db7-d4c1-45d6-b081-94b87779614f");
        internal static Guid WPD_CONTENT_TYPE_WIRELESS_PROFILE = new Guid("0bac070a-9f5f-4da4-a8f6-3de44d68fd6c");
        internal static Guid WPD_DEVICE_PROPERTIES_V1 = new Guid("26d4979a-e643-4626-9e2b-736dc0c92fdc");
        internal static Guid WPD_DEVICE_PROPERTIES_V2 = new Guid("463dd662-7fc4-4291-911c-7f4c9cca9799");
        internal static Guid WPD_DOCUMENT_OBJECT_PROPERTIES_V1 = new Guid("0b110203-eb95-4f02-93e0-97c631493ad5");
        internal static Guid WPD_EMAIL_OBJECT_PROPERTIES_V1 = new Guid("41f8f65a-5484-4782-b13d-4740dd7c37c5");
        internal static Guid WPD_EVENT_ATTRIBUTES_V1 = new Guid("10c96578-2e81-4111-adde-e08ca6138f6d");
        internal static Guid WPD_EVENT_DEVICE_CAPABILITIES_UPDATED = new Guid("36885aa1-cd54-4daa-b3d0-afb3e03f5999");
        internal static Guid WPD_EVENT_DEVICE_REMOVED = new Guid("e4cbca1b-6918-48b9-85ee-02be7c850af9");
        internal static Guid WPD_EVENT_DEVICE_RESET = new Guid("7755cf53-c1ed-44f3-b5a2-451e2c376b27");
        internal static Guid WPD_EVENT_OBJECT_ADDED = new Guid("a726da95-e207-4b02-8d44-bef2e86cbffc");
        internal static Guid WPD_EVENT_OBJECT_REMOVED = new Guid("be82ab88-a52c-4823-96e5-d0272671fc38");
        internal static Guid WPD_EVENT_OBJECT_TRANSFER_REQUESTED = new Guid("8d16a0a1-f2c6-41da-8f19-5e53721adbf2");
        internal static Guid WPD_EVENT_OBJECT_UPDATED = new Guid("1445a759-2e01-485d-9f27-ff07dae697ab");
        internal static Guid WPD_EVENT_OPTIONS_V1 = new Guid("b3d8dad7-a361-4b83-8a48-5b02ce10713b");
        internal static Guid WPD_EVENT_PROPERTIES_V1 = new Guid("15ab1953-f817-4fef-a921-5676e838f6e0");
        internal static Guid WPD_EVENT_PROPERTIES_V2 = new Guid("52807b8a-4914-4323-9b9a-74f654b2b846");
        internal static Guid WPD_EVENT_SERVICE_METHOD_COMPLETE = new Guid("8a33f5f8-0acc-4d9b-9cc4-112d353b86ca");
        internal static Guid WPD_EVENT_STORAGE_FORMAT = new Guid("3782616b-22bc-4474-a251-3070f8d38857");
        internal static Guid WPD_FOLDER_OBJECT_PROPERTIES_V1 = new Guid("7e9a7abf-e568-4b34-aa2f-13bb12ab177d");
        internal static Guid WPD_FORMAT_ATTRIBUTES_V1 = new Guid("a0a02000-bcaf-4be8-b3f5-233f231cf58f");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_ALL = new Guid("2d8a6512-a74c-448e-ba8a-f4ac07c49399");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_AUDIO_CAPTURE = new Guid("3f2a1919-c7c2-4a00-855d-f57cf06debbb");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_DEVICE = new Guid("08ea466b-e3a4-4336-a1f3-a44d2b5c438c");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_NETWORK_CONFIGURATION = new Guid("48f4db72-7c6a-4ab0-9e1a-470e3cdbf26a");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_RENDERING_INFORMATION = new Guid("08600ba4-a7ba-4a01-ab0e-0065d0a356d3");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_SMS = new Guid("0044a0b1-c1e9-4afd-b358-a62c6117c9cf");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_STILL_IMAGE_CAPTURE = new Guid("613ca327-ab93-4900-b4fa-895bb5874b79");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_STORAGE = new Guid("23f05bbc-15de-4c2a-a55b-a9af5ce412ef");
        internal static Guid WPD_FUNCTIONAL_CATEGORY_VIDEO_CAPTURE = new Guid("e23e5f6b-7243-43aa-8df1-0eb3d968a918");
        internal static Guid WPD_FUNCTIONAL_OBJECT_PROPERTIES_V1 = new Guid("8f052d93-abca-4fc5-a5ac-b01df4dbe598");
        internal static Guid WPD_IMAGE_OBJECT_PROPERTIES_V1 = new Guid("63d64908-9fa1-479f-85ba-9952216447db");
        internal static Guid WPD_MEDIA_PROPERTIES_V1 = new Guid("2ed8ba05-0ad3-42dc-b0d0-bc95ac396ac8");
        internal static Guid WPD_MEMO_OBJECT_PROPERTIES_V1 = new Guid("5ffbfc7b-7483-41ad-afb9-da3f4e592b8d");
        internal static Guid WPD_METHOD_ATTRIBUTES_V1 = new Guid("f17a5071-f039-44af-8efe-432cf32e432a");
        internal static Guid WPD_MUSIC_OBJECT_PROPERTIES_V1 = new Guid("b324f56a-dc5d-46e5-b6df-d2ea414888c6");
        internal static Guid WPD_NETWORK_ASSOCIATION_PROPERTIES_V1 = new Guid("e4c93c1f-b203-43f1-a100-5a07d11b0274");
        internal static Guid WPD_OBJECT_FORMAT_3GP = new Guid("b9840000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_AAC = new Guid("b9030000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_ABSTRACT_CONTACT = new Guid("bb810000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_ABSTRACT_CONTACT_GROUP = new Guid("ba060000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_ABSTRACT_MEDIA_CAST = new Guid("ba0b0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_AIFF = new Guid("30070000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_ALL = new Guid("c1f62eb2-4bb3-479c-9cfa-05b5f3a57b22");
        internal static Guid WPD_OBJECT_FORMAT_ASF = new Guid("300c0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_ASXPLAYLIST = new Guid("ba130000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_AUDIBLE = new Guid("b9040000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_AVI = new Guid("300a0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_BMP = new Guid("38040000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_CIFF = new Guid("38050000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_DPOF = new Guid("30060000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_EXECUTABLE = new Guid("30030000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_EXIF = new Guid("38010000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_FLAC = new Guid("b9060000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_FLASHPIX = new Guid("38030000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_GIF = new Guid("38070000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_HTML = new Guid("30050000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_ICALENDAR = new Guid("be030000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_ICON = new Guid("077232ed-102c-4638-9c22-83f142bfc822");
        internal static Guid WPD_OBJECT_FORMAT_JFIF = new Guid("38080000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_JP2 = new Guid("380f0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_JPX = new Guid("38100000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_M3UPLAYLIST = new Guid("ba110000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_M4A = new Guid("30aba7ac-6ffd-4c23-a359-3e9b52f3f1c8");
        internal static Guid WPD_OBJECT_FORMAT_MHT_COMPILED_HTML = new Guid("ba840000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MICROSOFT_EXCEL = new Guid("ba850000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MICROSOFT_POWERPOINT = new Guid("ba860000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MICROSOFT_WFC = new Guid("b1040000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MICROSOFT_WORD = new Guid("ba830000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MP2 = new Guid("b9830000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MP3 = new Guid("30090000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MP4 = new Guid("b9820000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MPEG = new Guid("300b0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_MPLPLAYLIST = new Guid("ba120000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_NETWORK_ASSOCIATION = new Guid("b1020000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_OGG = new Guid("b9020000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_PCD = new Guid("38090000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_PICT = new Guid("380a0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_PLSPLAYLIST = new Guid("ba140000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_PNG = new Guid("380b0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_PROPERTIES_ONLY = new Guid("30010000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_SCRIPT = new Guid("30020000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_TEXT = new Guid("30040000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_TIFF = new Guid("380d0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_TIFFEP = new Guid("38020000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_TIFFIT = new Guid("380e0000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_UNSPECIFIED = new Guid("30000000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_VCALENDAR1 = new Guid("be020000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_VCARD2 = new Guid("bb820000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_VCARD3 = new Guid("bb830000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_WAVE = new Guid("30080000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_WINDOWSIMAGEFORMAT = new Guid("b8810000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_WMA = new Guid("b9010000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_WMV = new Guid("b9810000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_WPLPLAYLIST = new Guid("ba100000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_X509V3CERTIFICATE = new Guid("b1030000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_FORMAT_XML = new Guid("ba820000-ae6c-4804-98ba-c57b46965fe7");
        internal static Guid WPD_OBJECT_PROPERTIES_V1 = new Guid("ef6b490d-5cd8-437a-affc-da8b60ee4a3c");
        internal static Guid WPD_PARAMETER_ATTRIBUTES_V1 = new Guid("e6864dd7-f325-45ea-a1d5-97cf73b6ca58");
        internal static Guid WPD_PROPERTY_ATTRIBUTES_V1 = new Guid("ab7943d8-6332-445f-a00d-8d5ef1e96f37");
        internal static Guid WPD_PROPERTY_ATTRIBUTES_V2 = new Guid("5d9da160-74ae-43cc-85a9-fe555a80798e");
        internal static Guid WPD_RENDERING_INFORMATION_OBJECT_PROPERTIES_V1 = new Guid("c53d039f-ee23-4a31-8590-7639879870b4");
        internal static Guid WPD_RESOURCE_ATTRIBUTES_V1 = new Guid("1eb6f604-9278-429f-93cc-5bb8c06656b6");
        internal static Guid WPD_SECTION_OBJECT_PROPERTIES_V1 = new Guid("516afd2b-c64e-44f0-98dc-bee1c88f7d66");
        internal static Guid WPD_SERVICE_PROPERTIES_V1 = new Guid("7510698a-cb54-481c-b8db-0d75c93f1c06");
        internal static Guid WPD_SMS_OBJECT_PROPERTIES_V1 = new Guid("7e1074cc-50ff-4dd1-a742-53be6f093a0d");
        internal static Guid WPD_STILL_IMAGE_CAPTURE_OBJECT_PROPERTIES_V1 = new Guid("58c571ec-1bcb-42a7-8ac5-bb291573a260");
        internal static Guid WPD_STORAGE_OBJECT_PROPERTIES_V1 = new Guid("01a3057a-74d6-4e80-bea7-dc4c212ce50a");
        internal static Guid WPD_TASK_OBJECT_PROPERTIES_V1 = new Guid("e354e95e-d8a0-4637-a03a-0cb26838dbc7");
        internal static Guid WPD_VIDEO_OBJECT_PROPERTIES_V1 = new Guid("346f2163-f998-4146-8b01-d19b4c00de9a");
    }
}
