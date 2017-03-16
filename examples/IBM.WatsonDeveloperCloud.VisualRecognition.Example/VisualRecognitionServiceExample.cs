﻿/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using System;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.Example
{

    public class VisualRecognitionServiceExample
    {
        private VisualRecognitionService _visualRecognition = new VisualRecognitionService();
        private string _imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Kittyply_edit1.jpg/1200px-Kittyply_edit1.jpg";
        public VisualRecognitionServiceExample(string apikey)
        {
            _visualRecognition.SetCredential(apikey);

            Classify();
        }

        private void Classify()
        {
            
            Console.WriteLine(string.Format("Calling Classify(\"{0}\")...", _imageUrl));
            var result = _visualRecognition.Classify(_imageUrl);

            if(result != null)
            {
                foreach (ClassifyTopLevelSingle image in result.Images)
                    foreach (ClassifyPerClassifier classifier in image.Classifiers)
                        foreach (ClassResult classResult in classifier.Classes)
                            Console.WriteLine(string.Format("class: {0} | score: {1} | type heirachy: {2}", classResult._Class, classResult.Score, classResult.TypeHierarchy));
            }
            else
            {
                Console.WriteLine("Classify() result is null.");
            }
        }
    }
}