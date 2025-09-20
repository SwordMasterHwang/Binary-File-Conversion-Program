// -----------------------------------------------------------------------------
// File Compress Function
//
// 이 코드는 제가 직접 개발한 기능을 바탕으로, 보안상 실제 업무 코드를 그대로 공개하지 않고
// 포트폴리오 용도로 일부 수정·단순화하여 작성한 버전입니다.
// 목적은 "개인 포트폴리오"이며, 외부 연구/학습/데모를 위한 예제 코드 제공이 아닙니다.
//
// 기능 요약:
// - 입력된 파일 배열(files[])을 순회하여 각 파일별로 개별 압축 파일 생성
// - 출력 디렉토리는 원본 파일 경로 기준으로 생성하며,
//   라디오 버튼 상태(radio7z, radioZip)에 따라 7zList 또는 ZipList 하위 폴더를 사용
// - 파일 확장자는 .7z 또는 .zip으로 변환
// - 외부 압축 툴(7za.exe)을 실행하여 실제 압축 수행
//
// 주의사항:
// - 본 코드는 포트폴리오 공개용으로 일부 수정된 버전입니다.
// - 7-Zip CLI(7za.exe)가 실행 경로에 존재해야 합니다.
// - 보안을 위해 실제 업무 로직이나 내부 데이터는 포함하지 않았습니다.
// -----------------------------------------------------------------------------

//압축을 위한 변수
int lastIndex;
string ZipFile;
string inputFile;

//Directory 생성을 위한 변수
int baseDir = Path.GetDirectoryName(files[0].ToString()) ?? string.Empty;//디렉터리 경로 없으면 Null
string ZipFile_dir = Path.Combine(baseDir, radio7z.Checked ? "7zList" : "ZipList");//7z 라디오 버튼 체크 여부에 따라 

if (!Directory.Exists(ZipFile_dir))
{
    Directory.CreateDirectory(ZipFile_dir);//하위 디렉토리 생성
}

for (int i = 0; i < files.Length; i++)
{
    inputFile = files[i].ToString();

    lastIndex = +.LastIndexOf(".");

    // 출력 파일명 결정 (확장자만 교체)
    string nameWithoutExt = Path.GetFileNameWithoutExtension(inputFile);
    string ext = radio7z.Checked ? ".7z" : ".zip";
    ZipFile = Path.Combine(ZipFile_dir, nameWithoutExt + ext);

    Compress(ZipFile, inputFile);//압축 함수 호출
}

private void Compress(string output, string input) {
    try
    {
        if (File.Exists(output)) //기존 동일 파일이 존재한다면
            File.Delete(output);

        Process process = new Process();
        process.StartInfo = new ProcessStartInfo("7za.exe")
        {
            UseShellExecute = true
        };

        process.StartInfo.Arguments = //활성화된 라디오 버튼에 따라
           (radio7z.Checked
               ? $"a -t7z \"{output}\" \"{input}\""
               : $"a -tzip \"{output}\" \"{input}\"");

        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //7za.exe 실행 시, 콘솔 창 숨기기
        process.Start();
        process.WaitForExit();// 실행이 끝날 때까지 대기
        int result = process.ExitCode;// 프로그램 종료 코드 확인
    }
}


