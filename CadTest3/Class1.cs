using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using CadTest3;
using System.Windows;

[assembly: CommandClass(typeof(Class1))]
namespace CadTest3
{

    public class Class1
    {
        [CommandMethod("adskgreeting")]
        public void Greeting()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            using(Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord rec;
                rec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                MText objText = new MText();
                objText.SetDatabaseDefaults();
                objText.Location = new Autodesk.AutoCAD.Geometry.Point3d(2, 2, 0);
                objText.Contents = "Pershendetje, Miresevini ne Autocad";
                objText.TextStyleId = acCurDb.Textstyle;
                rec.AppendEntity(objText);
                acTrans.AddNewlyCreatedDBObject(objText, true);
                acTrans.Commit();
            }
        }
        [CommandMethod("zvogelim")]
        public void Zvogelim()
        {
            Point p = new Point(0, 0);
            Application.MainWindow.DeviceIndependentLocation = p;
            Size sz = new Size(400, 400);
            Application.MainWindow.DeviceIndependentSize = sz;
        }
    }
}
