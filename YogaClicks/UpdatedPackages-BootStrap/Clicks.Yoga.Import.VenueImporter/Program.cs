using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Import.VenueImporter
{
    class Venue
    {
        public int? LegacyId;
        public string Name;
        public string Address1;
        public string Address2;
        public string Town;
        public string County;
        public string Postcode;
        public string Country;
        public string CountryCode;
        public double? Latitude;
        public double? Longitude;
        public string WebsiteUrl;
        public string Email;
        public string Telephone;
        public bool Residential;
        public string Type;
        public string Styles;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var venues = new List<Venue>();
            var directory = @"C:\Dropbox\PTYC\Content final\Final content for Beta site January 14 2014\Venues";
            var filePath = Path.Combine(directory, @"Venues database.xls");

            using (var con = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", filePath)))
            {
                con.Open();

                using (var cmd = new OleDbCommand("select * from [Venues for site$]", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var venue = new Venue();

                            venue.LegacyId = EnsureInt(reader["ID"]);
                            venue.Name = EnsureString(reader["Name"]);
                            venue.Address1 = EnsureString(reader["Address1"]);
                            venue.Address2 = EnsureString(reader["Address2"]);
                            venue.Town = EnsureString(reader["Town/City"]);
                            venue.County = EnsureString(reader["County/State"]);
                            venue.Postcode = EnsureString(reader["PostCode"]);
                            venue.Country = EnsureString(reader["Country"]);
                            venue.Latitude = EnsureDouble(reader["Latitude"]);
                            venue.Longitude = EnsureDouble(reader["Longitude"]);
                            venue.WebsiteUrl = EnsureString(reader["WebsiteUrl"]);
                            venue.Email = EnsureString(reader["Email"]);
                            venue.Telephone = EnsureString(reader["Telephone"]);
                            venue.Residential = EnsureBool(reader["Residential"]);
                            venue.Type = EnsureString(reader["Venue Type"]);
                            venue.Styles = EnsureString(reader["Styles"]) ?? "";

                            venues.Add(venue);
                        }
                    }
                }
            }

            using (var con = new SqlConnection("server=(local);database=YogaClicksImport;integrated security=true;"))
            {
                con.Open();

                using (var cmd = new SqlCommand("select Code from Country where Name = @name", con))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 500);

                    foreach (var venue in venues.Where(v => !string.IsNullOrEmpty(v.Country)))
                    {
                        var name = venue.Country;

                        if (name == "Bali")
                            name = "Indonesia";

                        if (name == "Scotland")
                            name = "United Kingdom";

                        if (name == "Tobago")
                            name = "Trinidad and Tobago";

                        if (name == "United States of America" || name == "USA")
                            name = "United States";

                        if (name == "Vietnam")
                            name = "Viet Nam";

                        if (name == "Wales")
                            name = "United Kingdom";

                        cmd.Parameters["@name"].Value = name;

                        var val = cmd.ExecuteScalar();

                        if (val != null && val != DBNull.Value)
                            venue.CountryCode = val.ToString();
                    }
                }

                using (var cmd = new SqlCommand("insert Venue (LegacyId, Name, Address1, Address2, Town, County, Postcode, Country, Latitude, Longitude, WebsiteUrl, Email, Telephone, Residential, Styles, Type) values (@id, @name, @address1, @address2, @town, @county, @postcode, @country, @latitude, @longitude, @websiteurl, @email, @telephone, @residential, @styles, @type)", con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@address1", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@address2", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@town", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@county", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@postcode", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@country", SqlDbType.NVarChar, 10);
                    cmd.Parameters.Add("@latitude", SqlDbType.Float);
                    cmd.Parameters.Add("@longitude", SqlDbType.Float);
                    cmd.Parameters.Add("@websiteurl", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@telephone", SqlDbType.NVarChar, 8000);
                    cmd.Parameters.Add("@residential", SqlDbType.Bit);
                    cmd.Parameters.Add("@styles", SqlDbType.NVarChar, 4000);
                    cmd.Parameters.Add("@type", SqlDbType.NVarChar, 4000);

                    foreach (var venue in venues)
                    {
                        cmd.Parameters["@id"].Value = ToDb(venue.LegacyId);
                        cmd.Parameters["@name"].Value = ToDb(venue.Name);
                        cmd.Parameters["@address1"].Value = ToDb(venue.Address1);
                        cmd.Parameters["@address2"].Value = ToDb(venue.Address2);
                        cmd.Parameters["@town"].Value = ToDb(venue.Town);
                        cmd.Parameters["@county"].Value = ToDb(venue.County);
                        cmd.Parameters["@postcode"].Value = ToDb(venue.Postcode);
                        cmd.Parameters["@country"].Value = ToDb(venue.CountryCode);
                        cmd.Parameters["@latitude"].Value = ToDb(venue.Latitude);
                        cmd.Parameters["@longitude"].Value = ToDb(venue.Longitude);
                        cmd.Parameters["@websiteurl"].Value = ToDb(venue.WebsiteUrl);
                        cmd.Parameters["@email"].Value = ToDb(venue.Email);
                        cmd.Parameters["@telephone"].Value = ToDb(venue.Telephone);
                        cmd.Parameters["@residential"].Value = venue.Residential;
                        cmd.Parameters["@styles"].Value = ToDb(venue.Styles);
                        cmd.Parameters["@type"].Value = ToDb(venue.Type);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static object ToDb(int? input)
        {
            if (input.HasValue)
                return input.Value;

            return DBNull.Value;
        }

        private static object ToDb(string input)
        {
            if (!string.IsNullOrEmpty(input))
                return input;

            return DBNull.Value;
        }

        private static object ToDb(double? input)
        {
            if (input.HasValue)
                return input.Value;

            return DBNull.Value;
        }

        private static int? EnsureInt(object value)
        {
            if (value is DBNull)
                return null;

            return Convert.ToInt32(value);
        }

        private static string EnsureString(object value)
        {
            if (value is DBNull) return null;
            return value.ToString();
        }

        private static bool EnsureBool(object value)
        {
            if (value is DBNull) return false;
            return (bool)value;
        }

        private static double? EnsureDouble(object value)
        {
            if (value is DBNull) return null;
            return (double?)value;
        }
    }
}
