#include "win32wrapper.hpp"

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static Settings settings;
	static fstream fileIO;

	switch (msg)
	{
	case WM_CREATE:
		readSettings(fileIO, settings);
		setLogFile(fileIO, settings);	
		break;

	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case ID_FILE_CHOOSEDIRECTORY:
			changeDirectory(settings.fileDirectory);
			fileIO.close();
			setLogFile(fileIO, settings);
			break;
		case ID_FILE_EXIT:
			break;
		case ID_TOOLS_SETTINGS:
		{
			int ret = DialogBox(GetModuleHandle(NULL),
				MAKEINTRESOURCE(IDD_DIALOG1), hwnd, SettingsDlgProc);
		}
			break;
		case ID_TOOLS_HELP:
			break;
		}
		break;
	case WM_CLOSE:
		DestroyWindow(hwnd);
		break;
	case WM_DESTROY:
		fileIO.close();
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hwnd, msg, wParam, lParam);
	}

	return 0;
}

BOOL CALLBACK SettingsDlgProc(HWND hwnd, UINT Message, WPARAM wParam, LPARAM lParam)
{
	switch (Message)
	{
	case WM_INITDIALOG:
		return TRUE;

	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDOK:
			EndDialog(hwnd, IDOK);
			break;
		case IDCANCEL:
			EndDialog(hwnd, IDCANCEL);
			break;
		}
		break;
	default:
		return FALSE;
	}
	return TRUE;
}


win32wrapper::win32wrapper(const char *g_szClassName, HINSTANCE hInstance, int nCmdShow, int width, int height)
{	
	//Do all the window stuff
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.style = 0;
	wc.lpfnWndProc = WndProc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hInstance = hInstance;
	wc.hIcon = LoadIcon(GetModuleHandle(NULL), MAKEINTRESOURCE(IDI_ICON1));
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wc.lpszMenuName = MAKEINTRESOURCE(IDR_MENU1);
	wc.lpszClassName = g_szClassName;
	wc.hIconSm = (HICON)LoadImage(GetModuleHandle(NULL), MAKEINTRESOURCE(IDI_ICON1), IMAGE_ICON, 32, 32, 0);

	if (!RegisterClassEx(&wc))
	{
		MessageBox(NULL, "Window Registration Failed!", "Error!",
			MB_ICONEXCLAMATION | MB_OK);
		exit(1);
	}

	hwnd = CreateWindowEx(
		WS_EX_CLIENTEDGE,
		g_szClassName,
		"TFREC IR Communication App",
		WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, CW_USEDEFAULT, width, height,
		NULL, NULL, hInstance, NULL);

	if (hwnd == NULL)
	{
		MessageBox(NULL, "Window Creation Failed!", "Error!",
			MB_ICONEXCLAMATION | MB_OK);
		exit(1);
	}

	ShowWindow(hwnd, nCmdShow);
	UpdateWindow(hwnd);
}

win32wrapper::~win32wrapper()
{

}

WNDCLASSEX win32wrapper::getWindowClass()
{
	return wc;
}

void win32wrapper::mainLoop()
{
	while (GetMessage(&msg, NULL, 0, 0) > 0)
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
}

MSG win32wrapper::getMessage()
{
	return msg;
}

bool fileExists(const string &fileName)
{
	std::ifstream infile(fileName);
	bool exists = infile.good();
	infile.close();
	return exists;
}

struct tm getTime()
{
	time_t t = time(NULL);
	return *localtime(&t);
}

string dateToString(struct tm currentTime)
{
	string date;
	char buffer[11];
	sprintf(buffer, "%02d-%02d-%d", currentTime.tm_mon + 1, currentTime.tm_mday, currentTime.tm_year + 1900);
	date = buffer;
	return date;
}

void changeDirectory(string &fileDirectory)
{
	BROWSEINFO bi = { 0 };
	bi.lpszTitle = "Choose Directory";
	LPITEMIDLIST pidl = SHBrowseForFolder(&bi);
	if (pidl != NULL)
	{
		TCHAR tszPath[MAX_PATH] = "\0";

		SHGetPathFromIDList(pidl, tszPath);
		
		fileDirectory = tszPath;

		CoTaskMemFree(pidl);
	}
}

void readSettings(fstream &infile, Settings &settings)
{
	//open settings.dat and read in relevant settings.
	infile.open("settings.dat", fstream::in);
	getline(infile, settings.fileDirectory);

	infile.close();
}

void setLogFile(fstream &flog, Settings &settings)
{
	if (fileExists(settings.fileDirectory + dateToString(getTime()) + ".csv"))
	{
		flog.open((settings.fileDirectory + dateToString(getTime()) + ".csv"), fstream::app);
	}

	else
	{
		flog.open((settings.fileDirectory + dateToString(getTime()) + ".csv"), fstream::out);
		flog << "(Hours:Minutes:Seconds), Ambient Temp(C), Object Temp(C)" << endl;
	}
}