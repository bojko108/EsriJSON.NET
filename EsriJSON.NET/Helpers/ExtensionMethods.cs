using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SOESupport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}
