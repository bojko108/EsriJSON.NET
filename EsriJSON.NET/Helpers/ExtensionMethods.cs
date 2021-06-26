using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SOESupport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EsriJSON.NET.Helpers
{
    internal static class ExtensionMethods
    {
        public static IPolyline ToPolyline(this IPath path)
        {
            IPolyline polyline = new PolylineClass();
            polyline.SpatialReference = path.SpatialReference;
            IGeometryCollection geometryCollection = polyline as IGeometryCollection;
            geometryCollection.AddGeometry(path);
            geometryCollection.GeometriesChanged();
            return polyline;
        }

        public static IPolygon ToPolygon(this IRing ring)
        {
            IPolygon polygon = new PolygonClass();
            polygon.SpatialReference = ring.SpatialReference;
            IGeometryCollection geometryCollection = polygon as IGeometryCollection;
            geometryCollection.AddGeometry(ring);
            geometryCollection.GeometriesChanged();
            return polygon;
        }

        public static List<JsonFeature> GetJsonFeatures(this ITable table, IQueryFilter filter)
        {
            List<JsonFeature> features = new List<JsonFeature>();

            try
            {
                string featureSetString = table.GetFeatureSetAsString(filter);
                JsonFeatureSet featureSet = JsonConvert.DeserializeObject<JsonFeatureSet>(featureSetString);
                features = featureSet.Features;
            }
            catch
            {

            }

            return features;
        }

        public static string GetFeatureSetAsString(this ITable table, IQueryFilter filter)
        {
            IRecordSet recordset = new RecordSet();
            IRecordSetInit recordSetInit = recordset as IRecordSetInit;

            recordSetInit.SetSourceTable(table, filter);

            byte[] jsonBytes = Conversion.ToJson(recordset);

            return Encoding.UTF8.GetString(jsonBytes);
        }

        /// <summary>
        /// Get the records from a Feature Cursor object
        /// </summary>
        /// <param name="featureCursor">Feature Cursor object containig the records</param>
        /// <param name="releaseCursor">If true, the Cursor will be released at the end</param>
        /// <returns>A List of records</returns>
        public static List<IFeature> GetFeatures(this IFeatureCursor featureCursor, bool releaseCursor = true)
        {
            return featureCursor.GetFeatures(f => f != null, releaseCursor);
        }

        /// <summary>
        /// Get the records from a Feature Cursor object with a predicate function executed for every record
        /// </summary>
        /// <param name="featureCursor">Feature Cursor object containig the records</param>
        /// <param name="featurePredicate">Predicate function, which tests the records</param>
        /// <param name="releaseCursor">If true, the Cursor will be released at the end</param>
        /// <returns>A List of records containing only the records satisfying the predicate function</returns>
        public static List<IFeature> GetFeatures(this IFeatureCursor featureCursor, Predicate<IFeature> featurePredicate, bool releaseCursor = true)
        {
            List<IFeature> result = new List<IFeature>();

            try
            {
                IFeature feature = null;

                while ((feature = featureCursor.NextFeature()) != null)
                {
                    if (featurePredicate(feature))
                        result.Add(feature);
                }
            }
            finally
            {
                if (releaseCursor)
                {
                    Marshal.ReleaseComObject(featureCursor);
                }
            }

            return result;
        }

        /// <summary>
        /// Get the records from a Cursor object
        /// </summary>
        /// <param name="cursor">Cursor object containig the records</param>
        /// <param name="releaseCursor">If true, the Cursor will be released at the end</param>
        /// <returns>A List of records</returns>
        public static List<IRow> GetRows(this ICursor cursor, bool releaseCursor = true)
        {
            return cursor.GetRows(r => r != null, releaseCursor);
        }

        /// <summary>
        /// Get the records from a Cursor object with a predicate function executed for every record
        /// </summary>
        /// <param name="cursor">Cursor object containig the records</param>
        /// <param name="rowPredicate">Predicate function, which tests the records</param>
        /// <param name="releaseCursor">If true, the Cursor will be released at the end</param>
        /// <returns>A List of records containing only the records satisfying the predicate function</returns>
        public static List<IRow> GetRows(this ICursor cursor, Predicate<IRow> rowPredicate, bool releaseCursor = true)
        {
            List<IRow> result = new List<IRow>();

            try
            {
                IRow row = null;

                while ((row = cursor.NextRow()) != null)
                {
                    if (rowPredicate(row))
                        result.Add(row);
                }
            }
            finally
            {
                if (releaseCursor)
                {
                    Marshal.ReleaseComObject(cursor);
                }
            }

            return result;
        }


        /// <summary>
        ///     Returns the description for the specified field from the <paramref name="source" /> (if linked to a domain).
        /// </summary>
        /// <param name="source">The row.</param>
        /// <param name="index">The index.</param>
        /// <param name="fallbackValue">The default value.</param>
        /// <returns>
        ///     Returns a <see cref="string" /> representing the converted value to the specified type.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static string Get(this IRow source, int index, string fallbackValue = null)
        {
            if (source == null) return fallbackValue;
            if (index < 0 || index > source.Fields.FieldCount - 1)
                throw new IndexOutOfRangeException();

            IField field = source.Fields.Field[index];
            if (field.Domain is ICodedValueDomain domain)
            {
                return domain.GetDescription(source.Value[index]);
            }
            else
            {
                return TypeCast.Cast(source.Value[index], fallbackValue);
            }
        }

        /// <summary>
        ///     Finds the description in the domain that matches the specified <paramref name="value" />
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        /// <returns>Returns a <see cref="string" /> representing the name (or description) otherwise <c>null</c>.</returns>
        public static string GetDescription(this ICodedValueDomain source, object value)
        {
            if ((source == null) || (value == null) || Convert.IsDBNull(value))
            {
                return null;
            }

            return (from entry in source.AsEnumerable() where entry.Value.Equals(value.ToString()) select entry.Key).FirstOrDefault();
        }

        /// <summary>
        ///     Creates an <see cref="IEnumerable{T}" /> from an <see cref="ICodedValueDomain" />
        /// </summary>
        /// <param name="source">An <see cref="ICodedValueDomain" /> to create an <see cref="IEnumerable{T}" /> from.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> that contains the domain from the input source.</returns>
        public static IEnumerable<KeyValuePair<string, string>> AsEnumerable(this ICodedValueDomain source)
        {
            if (source != null)
            {
                for (int i = 0; i < source.CodeCount; i++)
                {
                    yield return new KeyValuePair<string, string>(source.Name[i], TypeCast.Cast(source.Value[i], string.Empty));
                }
            }
        }
    }
}
