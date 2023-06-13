import { shallowMount } from '@vue/test-utils'
import OdabirMentora from '@/views/HomePageViews/OdabirMentora'
import userController from '@/controllerEndpoints/userController'
import thesisController from '@/controllerEndpoints/thesisController'
import expect from 'expect'
import * as jest from 'node/test'
import { afterEach, beforeEach, describe, it } from 'node:test'

jest.mock('@/controllerEndpoints/userController', () => ({
  getAllMentors: jest.fn(() => Promise.resolve({ data: [] }))
}))

jest.mock('@/controllerEndpoints/thesisController', () => ({
  addThesis: jest.fn(() => Promise.resolve())
}))

describe('OdabirMentora', () => {
  let wrapper

  beforeEach(() => {
    wrapper = shallowMount(OdabirMentora)
  })

  afterEach(() => {
    wrapper.destroy()
  })

  it('calls getAllMentors method on mount', async () => {
    const getAllMentors = jest.spyOn(wrapper.vm, 'getAllMentors')
    await wrapper.vm.$nextTick()
    expect(getAllMentors).toHaveBeenCalled()
  })

  it('initializes mentorList data property as an empty array', () => {
    expect(wrapper.vm.mentorList).toEqual([])
  })

  it('displays mentor list table with the correct number of columns', () => {
    const columns = wrapper.findAllComponents({ name: 'Column' })
    expect(columns).toHaveLength(6)
  })

  it('calls userController.getAllMentors and sets mentorList data property', async () => {
    const mockMentors = [{ id: 1, firstName: 'Emil', lastName: 'Krulcic' }]
    userController.getAllMentors.mockResolvedValueOnce({ data: mockMentors })
    await wrapper.vm.getAllMentors()
    expect(wrapper.vm.mentorList).toEqual(mockMentors)
  })

  it('calls thesisController.addThesis and shows success toast on mentor select', async () => {
    const mockCourseId = 1
    const mockUserId = 2
    const toastAdd = jest.spyOn(wrapper.vm.$toast, 'add')
    await wrapper.vm.onMentorSelect(mockCourseId)
    expect(thesisController.addThesis).toHaveBeenCalledWith(mockCourseId, mockUserId)
    expect(toastAdd).toHaveBeenCalledWith({
      severity: 'success',
      summary: 'Uspješno',
      detail: 'Mentoru je poslan zahtjev za mentorstvo!',
      life: 3000
    })
  })

  it('shows error toast if thesisController.addThesis fails on mentor select', async () => {
    thesisController.addThesis.mockRejectedValueOnce()
    const toastAdd = jest.spyOn(wrapper.vm.$toast, 'add')
    await wrapper.vm.onMentorSelect(1)
    expect(toastAdd).toHaveBeenCalledWith({
      severity: 'error',
      summary: 'Greška',
      detail: 'Došlo je do greške, pokušajte kasnije!',
      life: 3000
    })
  })
})
