protected override void OnEndManipulation(TapGesture gesture)
        {
            if (gesture.WasCancelled)
            {
                return;
            }

            if (ManipulationSystem.Instance == null)
            {
                return;
            }

            GameObject target = gesture.TargetObject;
            if (target == gameObject)
            {
                
                string jsn = "{\"username\":\"kush\",\"description\":\"" + target.transform.position.ToString() + "\",\"duration\":5000,\"date\":\"2020-06-06T10:33:01.790Z\"}";
                Hashtable postHeader = new Hashtable();
                postHeader.Add("Content-Type", "application/json");

                var formData = System.Text.Encoding.UTF8.GetBytes(jsn);



                string url = "https://mernactivitytracker.herokuapp.com/exercises/add";



                var jsonBinary = System.Text.Encoding.UTF8.GetBytes(jsn);

                DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

                UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
                uploadHandlerRaw.contentType = "application/json";

                UnityWebRequest www =
                    new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

                www.SendWebRequest();

                if (www.isNetworkError)
                    Debug.LogError(string.Format("{0}: {1}", www.url, www.error));
                else
                    Debug.Log(string.Format("Response: {0}", www.downloadHandler.text));

                if (target.transform.position.y <= 0)
                {
                    Application.OpenURL("http://192.168.43.141:1337/L");  //for sending string to nodemcu
                }
                
                else
                {
                    Application.OpenURL("http://192.168.43.141:1337/H");
                }

                Select();
            }

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;

            if (!Frame.Raycast(
                gesture.StartPosition.x, gesture.StartPosition.y, raycastFilter, out hit))
            {
                Deselect();
            }
        }
