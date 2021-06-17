import time
import pyautogui
from random import randint
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
fraseSeed = "Viverra mauris in aliquam sem. Adipiscing at in tellus integer feugiat scelerisque varius morbi enim. Tempus egestas sed sed risus pretium. Blandit massa enim nec dui nunc mattis enim ut. Nullam vehicula ipsum a arcu cursus vitae congue. Mattis enim ut tellus elementum sagittis vitae et leo. Pellentesque eu tincidunt tortor aliquam nulla facilisi cras. Viverra justo nec ultrices dui sapien. Aliquam id diam maecenas ultricies mi eget mauris pharetra et. Fringilla ut morbi tincidunt augue interdum. Porttitor massa id neque aliquam vestibulum. Eget dolor morbi non arcu risus quis varius quam quisque. Sed tempus urna et pharetra pharetra massa massa ultricies. Vitae ultricies leo integer malesuada nunc vel risus commodo. Dignissim diam quis enim lobortis scelerisque fermentum dui faucibus in. Ut sem viverra aliquet eget sit. Sagittis aliquam malesuada bibendum arcu vitae. Nisi vitae suscipit tellus mauris a diam maecenas.Diam phasellus vestibulum lorem sed risus ultricies. Erat nam at lectus urna duis convallis convallis tellus. Fermentum odio eu feugiat pretium nibh ipsum consequat nisl. Eu augue ut lectus arcu bibendum. Risus sed vulputate odio ut enim blandit volutpat maecenas volutpat. Etiam non quam lacus suspendisse. Eget egestas purus viverra accumsan in nisl nisi scelerisque eu. Ultrices sagittis orci a scelerisque purus semper eget duis at. Mi sit amet mauris commodo quis imperdiet massa. Nisl condimentum id venenatis a condimentum vitae sapien. At elementum eu facilisis sed odio morbi quis commodo odio. Nisi porta lorem mollis aliquam. Id diam vel quam elementum. Eu volutpat odio facilisis mauris sit amet massa vitae. Tincidunt ornare massa eget egestas purus viverra accumsan."

def gerarFrase(tamanho):
    if(tamanho > len(fraseSeed)): tamanho = len(fraseSeed)
    inicio = randint(0, len(fraseSeed) - tamanho)
    return fraseSeed[inicio: inicio + tamanho]

def contains(array, titulo):
	for i in range(len(array)):
		if(array[i][0] == titulo):return i
	return None

def test(amostra, titulo, descricao, i):
	assert amostra[0] == titulo and (amostra[1] == descricao), "Teste " + str(i) +  " raealizado com insucesso, descricao ou titulo errados: " + titulo + " : " + descricao

def trim(frase):
	while(frase.endswith(" ")):frase = frase[:len(frase) - 1]
	while (frase.startswith(" ")): frase = frase[1:]
	return frase

service = Service('chromedriver.exe')
service.start()
driver = webdriver.Remote(service.service_url)
driver.get('https://luiza-andaluz.azurewebsites.net/');
time.sleep(2)
memoria=[]

#Registar
pyautogui.click(725, 186)
time.sleep(3)
#Email
pyautogui.click(150, 437)
pyautogui.write('teste', interval=0.01)
time.sleep(1)
pyautogui.hotkey('altright','2')
time.sleep(1)
pyautogui.write('teste.pt', interval=0.01)
time.sleep(1)
#Password
pyautogui.click(150, 526)
pyautogui.hotkey('shift','3')
pyautogui.write('Asdqwe123', interval=0.01)
#Password Confirmação
pyautogui.click(150, 612)
pyautogui.hotkey('shift','3')
pyautogui.write('Asdqwe123', interval=0.01)
#Criar
pyautogui.click(184, 665)
time.sleep(3)
#Logout
pyautogui.click(800, 186)
time.sleep(3)
#Login 
pyautogui.click(800, 186)
time.sleep(3)
#Email
pyautogui.click(160, 441)
time.sleep(1)
pyautogui.write('admin', interval=0.01)
time.sleep(1)
pyautogui.hotkey('altright','2')
time.sleep(1)
pyautogui.write('admin.pt', interval=0.01)
time.sleep(1)
#Pass
pyautogui.click(160, 523)
time.sleep(1)
pyautogui.write('Qwe123#', interval=0.01)
time.sleep(1)
#Entrar na conta admin
pyautogui.click(177, 629)
time.sleep(1)
#Admitir
pyautogui.click(545, 161)
time.sleep(1)
pyautogui.click(536, 462)
time.sleep(1)
pyautogui.click(429, 514)
#Confirmar a admição 
pyautogui.click(710, 460)
time.sleep(1)
#Logout da conta admin
pyautogui.click(905, 187)
time.sleep(1)
#Login com a conta teste "Irmã"
pyautogui.click(800, 186)
time.sleep(1)
#Email
pyautogui.click(160, 441)
time.sleep(1)
pyautogui.write('teste', interval=0.01)
time.sleep(1)
pyautogui.hotkey('altright','2')
time.sleep(1)
pyautogui.write('teste.pt', interval=0.01)
time.sleep(1)
#Password
pyautogui.click(160, 523)
time.sleep(1)
pyautogui.hotkey('shift','3')
pyautogui.write('Asdqwe123', interval=0.01)
time.sleep(1)
#Entrar na conta testes
pyautogui.click(177, 629)
time.sleep(1)
#Contar a minha História
pyautogui.click(323, 185)
time.sleep(3)
#Clicar no mapa
pyautogui.moveTo(randint(129, 403), randint(782, 782))
pyautogui.click()
time.sleep(1)
#Titulo
pyautogui.click(150, 900)
time.sleep(1)
pyautogui.write('Teste', interval=0.01)
pyautogui.scroll(-700)
time.sleep(1)
#Testemunho
pyautogui.click(150, 460)
time.sleep(1)
pyautogui.write('Teste Teste Teste Teste Teste', interval=0.01)
#Nome
pyautogui.click(150,690)
pyautogui.write('Teste Teste', interval=0.01)
#DataNascimento
pyautogui.click(793,771)
pyautogui.click(793,771)
time.sleep(1)
pyautogui.click(238,642)
#Email
pyautogui.click(147,856)
time.sleep(1)
pyautogui.write('teste', interval=0.01)
time.sleep(1)
pyautogui.hotkey('altright','2')
time.sleep(1)
pyautogui.write('teste.pt', interval=0.01)
time.sleep(1)
#CriarHistoria
pyautogui.click(188,910)
time.sleep(1)
#Ver História
pyautogui.click(435,149)
time.sleep(1)
pyautogui.click(155,400)
time.sleep(1)
pyautogui.write('Teste', interval=0.01)
time.sleep(1)
pyautogui.click(430,475)
time.sleep(1)
pyautogui.click(475,685)
time.sleep(15)

driver.quit()