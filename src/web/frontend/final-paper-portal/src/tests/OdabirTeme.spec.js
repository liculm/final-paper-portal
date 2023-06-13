import { shallowMount } from '@vue/test-utils'
import OdabirTeme from '@/views/HomePageViews/OdabirTeme'
import thesisController from '@/controllerEndpoints/thesisController'
import expect from 'expect'
import * as jest from 'node/test'
import { afterEach, beforeEach, describe, it } from 'node:test'

jest.mock('@/controllerEndpoints/thesisController', () => ({
  selectThesisName: jest.fn(() => Promise.resolve())
}))

describe('OdabirTeme', () => {
  let wrapper

  beforeEach(() => {
    wrapper = shallowMount(OdabirTeme)
  })

  afterEach(() => {
    wrapper.destroy()
  })

  it('sets the enteredTopic data property correctly when input value changes', async () => {
    const input = wrapper.find('input[type="text"]')
    await input.setValue('Test tema')
    expect(wrapper.vm.enteredTopic).toBe('Test tema')
  })

  it('disables the button when topicValidation is false', () => {
    wrapper.setData({ enteredTopic: '' })
    const button = wrapper.find('button')
    expect(button.attributes('disabled')).toBe('disabled')
  })

  it('enables the button when topicValidation is true', () => {
    wrapper.setData({ enteredTopic: 'Test tema' })
    const button = wrapper.find('button')
    expect(button.attributes('disabled')).toBeFalsy()
  })

  it('calls thesisController.selectThesisName and shows success toast on selecting a thesis name', async () => {
    const mockUserId = 1
    const mockEnteredTopic = 'Test tema'
    const toastAdd = jest.spyOn(wrapper.vm.$toast, 'add')
    await wrapper.setData({ enteredTopic: mockEnteredTopic })
    await wrapper.vm.selectThesisName()
    expect(thesisController.selectThesisName).toHaveBeenCalledWith(mockUserId, mockEnteredTopic)
    expect(toastAdd).toHaveBeenCalledWith({
      severity: 'success',
      summary: 'Uspješno',
      detail: 'Uspješno ste postavili temu',
      life: 3000
    })
  })

  it('shows error toast if thesisController.selectThesisName fails on selecting a thesis name', async () => {
    thesisController.selectThesisName.mockRejectedValueOnce()
    const toastAdd = jest.spyOn(wrapper.vm.$toast, 'add')
    await wrapper.setData({ enteredTopic: 'Test tema' })
    await wrapper.vm.selectThesisName()
    expect(toastAdd).toHaveBeenCalledWith({
      severity: 'error',
      summary: 'Greška',
      detail: 'Greška prilikom postavljanja teme',
      life: 3000
    })
  })
})
