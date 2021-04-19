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
for i in range(5):
#Contar a minha história
	pyautogui.click(379, 163)
	time.sleep(5)
#Carregar ok
	pyautogui.click(600, 512)
	time.sleep(1)
#Clicar no mapa
	pyautogui.moveTo(randint(170, 765), randint(425, 687))
	pyautogui.click()
	time.sleep(1)
#Fim do mapa
	time.sleep(1)
	pyautogui.click(143, 868)
	time.sleep(1)
#Titulo
	titulo=trim(gerarFrase(randint(5,20)))
	pyautogui.write(titulo, interval=0.01)
	time.sleep(1)
	pyautogui.click(143, 947)
	time.sleep(1)
#Descrição
	descricao=trim(gerarFrase(randint(50,200)))
	pyautogui.write(descricao, interval=0.01)
	time.sleep(1)
	pyautogui.scroll(-700)
	time.sleep(1)
	pyautogui.click(143, 684)
	time.sleep(1)
#Nome
	pyautogui.write("Luiza Andaluz", interval=0.01)
	time.sleep(1)
	pyautogui.click(143, 770)
	time.sleep(1)
#Idade
	pyautogui.write(str(randint(15,80)), interval=0.01)
	time.sleep(1)
	pyautogui.click(143, 855)
	time.sleep(1)
#email
	pyautogui.write(gerarFrase(randint(30,50)), interval=0.01)
	time.sleep(1)
#Clicar no button create
	pyautogui.click(170, 911)
	time.sleep(3)
	memoria.append([titulo, descricao])
	print("Historia " + str(i+1) + " criada")
#login
pyautogui.click(790, 163)
time.sleep(3)
pyautogui.click(152, 390)
#Email
pyautogui.write('admin', interval=0.01)
time.sleep(1)
pyautogui.hotkey('altright','2')
time.sleep(1)
pyautogui.write('admin.pt', interval=0.01)
time.sleep(1)
#Pass
pyautogui.click(152, 473)
pyautogui.write('Qwe123#', interval=0.01)
time.sleep(1)
#Login
pyautogui.click(171, 580)
print("Login realizado")
time.sleep(2)
#Histórias por validar
pyautogui.click(493, 173)
time.sleep(5)
i = 1
while(len(memoria) > 0):
	titulo = None
	descricao = None
	try:
		titulo = driver.find_element_by_xpath("/html/body/div/main/table/tbody/tr["+str(i)+"]/td[1]").text
		descricao = driver.find_element_by_xpath("/html/body/div/main/table/tbody/tr["+str(i)+"]/td[2]").text
	except:
		break
	index = contains(memoria, titulo)
	if(index != None):
		test(memoria[index], titulo, descricao, i)
		print("Teste da historia " + str(i) + " realizado com sucesso")
		memoria.pop(index)
	i += 1

print("Todos os testes foram passados com sucesso")
driver.quit()
