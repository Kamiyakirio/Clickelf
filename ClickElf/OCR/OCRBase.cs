using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;
using Point = System.Drawing.Point;

namespace ClickElf.OCR
{
    class OCRBase
    {
        public static void Do()
        {
            // 截取屏幕图像
            Bitmap screenshot = CaptureScreen();
            Mat image = BitmapConverter.ToMat(screenshot);
            if (image.Channels() == 4)
            {
                image = image.CvtColor(ColorConversionCodes.BGRA2BGR);
            }

            // 加载 DNN 人脸检测模型
            string modelPath = "res10_300x300_ssd_iter_140000_fp16.caffemodel";
            string configPath = "deploy.prototxt";
            Net net = CvDnn.ReadNetFromCaffe(configPath, modelPath);

            // 进行人脸检测
            DetectFacesDNN(image, net);

            // 显示识别结果
            Cv2.ImShow("Detected Faces", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private static Bitmap CaptureScreen()
        {
            Rectangle bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
            }
            return bitmap;
        }

        private static void DetectFacesDNN(Mat image, Net net)
        {
            Mat blob = CvDnn.BlobFromImage(image, 1.0, new OpenCvSharp.Size(300, 300), new Scalar(104, 177, 123), false, false);
            net.SetInput(blob);
            Mat detections = net.Forward();
            Mat faces = detections.Reshape(1, detections.Size(2));

            faces.GetArray(out float[] data);
            int dimensions = 7;

            for (int i = 0; i < faces.Rows; i++)
            {
                float confidence = data[i * dimensions + 2];
                if (confidence > 0.5)
                {
                    int x1 = (int)(data[i * dimensions + 3] * image.Cols);
                    int y1 = (int)(data[i * dimensions + 4] * image.Rows);
                    int x2 = (int)(data[i * dimensions + 5] * image.Cols);
                    int y2 = (int)(data[i * dimensions + 6] * image.Rows);
                    Cv2.Rectangle(image, new Rect(x1, y1, x2 - x1, y2 - y1), Scalar.Red, 2);
                    Cv2.PutText(image, $"{confidence:P1}", new OpenCvSharp.Point(x1, y1 - 10), HersheyFonts.HersheySimplex, 0.5, Scalar.Green, 2);
                }
            }
        }
    }
}