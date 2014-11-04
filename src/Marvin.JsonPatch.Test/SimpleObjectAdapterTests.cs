﻿using Marvin.JsonPatch.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.Test
{
    [TestClass]
    public class SimpleObjectAdapterTests
    {

        [TestMethod]
        public void AddResultsInReplace()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<string>(o => o.StringProperty, "B");

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("B", doc.StringProperty);
          
        }


        [TestMethod]
        public void AddToList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 0);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 4, 1, 2, 3 }, doc.IntegerList);
        }


        [TestMethod]
        public void AddToListInvalidPositionTooLarge()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, 3);


            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException) { }

        }


        [TestMethod]
        public void AddToListInvalidPositionTooSmall()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4, -1);

            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException) 
            { 
            }



        }

        [TestMethod]
        public void AddToListAppend()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Add<int>(o => o.IntegerList, 4);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 4 }, doc.IntegerList);

        }


        [TestMethod]
        public void Remove()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<string>(o => o.StringProperty); 

            patchDoc.ApplyTo(doc);

            Assert.AreEqual(null, doc.StringProperty);

        }



        [TestMethod]
        public void RemoveFromList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, 2);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2 }, doc.IntegerList);
        }


        [TestMethod]
        public void RemoveFromListInvalidPositionTooLarge()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList, 3);


            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException) { }

        }


        [TestMethod]
        public void RemoveFromListInvalidPositionTooSmall()
        {

            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList,-1);

            try
            {
                patchDoc.ApplyTo(doc);

                // if we get here, we should fail b/c no exception was thrown
                Assert.Fail();

            }
            catch (JsonPatchException)
            {
            }



        }

        [TestMethod]
        public void RemoveFromEndOfList()
        {
            var doc = new SimpleDTO()
            {
                IntegerList = new List<int>() { 1, 2, 3 }
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Remove<int>(o => o.IntegerList);

            patchDoc.ApplyTo(doc);

            CollectionAssert.AreEquivalent(new List<int>() { 1, 2 }, doc.IntegerList);

        }


        [TestMethod]
        public void Replace()
        {
            var doc = new SimpleDTO()
            {
                StringProperty = "A"
            };

            // create patch
            JsonPatchDocument<SimpleDTO> patchDoc = new JsonPatchDocument<SimpleDTO>();
            patchDoc.Replace<string>(o => o.StringProperty, "B");

            patchDoc.ApplyTo(doc);

            Assert.AreEqual("B", doc.StringProperty);

        }

    }
}