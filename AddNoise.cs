// -----------------------------------------------------------------------------
// White Noise Addition Function
//
// 이 코드는 제가 직접 작성한 기능의 일부를 발췌하여 공개한 것입니다.
// 목적은 "개인 포트폴리오"이며, 외부 연구/학습/데모를 위한 예제 코드 제공이 아닙니다.
//
// 기능 요약:
// - 다채널 데이터(DataRaw)에 화이트 노이즈를 추가합니다.
// - 진폭(amplitude)을 지정하여 노이즈 세기를 조절할 수 있습니다.
// - 내부적으로 System.Random을 사용하여 난수를 발생시킵니다.
//
// 주의사항:
// - 주석에 포함된 NationalInstruments.Analysis.SignalGeneration.WhiteNoiseSignal 관련 코드는
//   상용 라이브러리 사용 예시이며, 실제 활용 시 별도의 라이선스가 필요합니다.
// - 이 파일에는 NI 라이브러리가 포함되어 있지 않습니다.
// -----------------------------------------------------------------------------

private void AddNoise()
        {
            float sampleRate = sampleNumeric; //****** 샘플 주파수 (Hz)
            float durationInSeconds = noiseSeconds;//White Noise 길이 (초)
            int block = blockCnt;// 블럭 수

            int totalSample = Convert.ToInt32(sampleRate * durationInSeconds * block);

            Random random = new Random();//Random 클래스

            float[,] whiteNoise = new float[chcnt, totalSample];//whiteNoise 수, 채널 수

            for (int i = 0; i < chcnt; i++)
            {
                for (int j = 0; j < DataRaw[0].Count; j++)//****** 데이터 개수까지
                {
                    whiteNoise[i, j] += Convert.ToSingle(random.NextDouble() * (amplitude * 2) - amplitude); // 진폭 입력대로
                    DataRaw[i][j] += whiteNoise[i, j];//noise 추가
                }
            }

            //아래는 NI 라이센스 필요한 WhiteNoise 생성 코드
            ////WhiteNoiseSignal : (진폭), (난수) - 패턴의 시작점, 난수(seed) 미지정 시 시스템 시간으로 자동 설정
            //NationalInstruments.Analysis.SignalGeneration.WhiteNoiseSignal wns = new NationalInstruments.Analysis.SignalGeneration.WhiteNoiseSignal(0.05);

            //for (int i = 0; i < chcnt; i++)
            //{
            //    {
            //    var temp = wns.Generate(sampling, DataRaw[0].Count);//****** 샘플링,샘플 수
            //    for (int j = 0; j < DataRaw[0].Count; j++)
            //        DataRaw[i][j] += Convert.ToSingle(temp[j]);
            //    }
            //}

        }
        

