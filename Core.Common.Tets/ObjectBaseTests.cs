using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common.Tests;
using System.ComponentModel;
using Core.Common.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Core.Common.Tets
{
    [TestClass]
    public class ObjectBaseTests
    {
        [TestMethod]
        public void test_clean_property_change()
        {
            TestClass objectClass = new TestClass();
            bool propertyChange = false;
            objectClass.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "CleanProp")
                    propertyChange = true;
            };
            objectClass.CleanProp = "Clean property";
            Assert.IsTrue(propertyChange, "The property should have triggered property chnage notification");
        }

        [TestMethod]
        public void test_dirty_set()
        {
            TestClass obj = new TestClass();
            Assert.IsFalse(obj.IsDirty, "Object should be clean");
            obj.DirtyProp = "Set dirty";
            Assert.IsTrue(obj.IsDirty, "Object should be dirty");
        }

        [TestMethod]
        public void test_property_change_single_subscription()
        {
            TestClass objTest = new TestClass();
            int changeCounter = 0;
            PropertyChangedEventHandler handler1 = new PropertyChangedEventHandler((s, e) => { changeCounter++; });
            PropertyChangedEventHandler handler2 = new PropertyChangedEventHandler((s, e) => { changeCounter++; });

            objTest.PropertyChanged += handler1;
            objTest.PropertyChanged += handler1; // should not duplicate
            objTest.PropertyChanged += handler1; // should not duplicate
            objTest.PropertyChanged += handler2;
            objTest.PropertyChanged += handler2; // should not duplicate

            objTest.CleanProp = "test value";

            Assert.IsTrue(changeCounter == 2, "Property change notification should only have been called once.");
            Assert.IsFalse(changeCounter > 2, "Property change notification should not be greater than two.");
        }

        [TestMethod]
        public void test_object_validation()
        {
            TestClass objTest = new TestClass();
            Assert.IsFalse(objTest.IsValid, "On initialization of the class isvalid should be false");
            Assert.IsFalse(objTest.IsDirty, "On initialization of the class isdirty should be false");
            objTest.StringProp = "Should be vald now";
            objTest.DirtyProp = "Should be dirty now";
            Assert.IsTrue(objTest.IsValid);

            objTest.StringProp = "Should be dirty now";
            objTest.DirtyProp = "Should Be Dirty Now";
            Assert.IsFalse(objTest.IsValid);
            Assert.IsTrue(objTest.ValidationErrors.Count() > 0, "DirtyProp is equal to StringProp");
            Assert.AreEqual(objTest.ValidationErrors.First().ErrorMessage, "\"Dirty prop\" should not be equal to \"String prop\"");

            var list = objTest.ValidationErrors.ToList();
        }

        [TestMethod]
        public void test_child_dirty_tracking()
        {
            TestClass objTest = new TestClass();

            Assert.IsFalse(objTest.IsAnythingDirty(), "Nothing in the object graph should be dirty.");

            objTest.Child.ChildName = "test value";

            Assert.IsTrue(objTest.IsAnythingDirty(), "The object graph should be dirty.");

            objTest.CleanAll();

            Assert.IsFalse(objTest.IsAnythingDirty(), "Nothing in the object graph should be dirty.");
        }

        [TestMethod]
        public void test_dirty_object_aggregating()
        {
            TestClass objTest = new TestClass();

            List<IDirtyCapable> dirtyObjects = objTest.GetDirtyObjects();

            Assert.IsTrue(dirtyObjects.Count == 0, "There should be no dirty object returned.");

            objTest.Child.ChildName = "test value";
            dirtyObjects = objTest.GetDirtyObjects();

            Assert.IsTrue(dirtyObjects.Count == 1, "There should be one dirty object.");

            objTest.DirtyProp = "test value";
            dirtyObjects = objTest.GetDirtyObjects();

            Assert.IsTrue(dirtyObjects.Count == 2, "There should be two dirty object.");

            objTest.CleanAll();
            dirtyObjects = objTest.GetDirtyObjects();

            Assert.IsTrue(dirtyObjects.Count == 0, "There should be no dirty object returned.");
        }
    }
}
